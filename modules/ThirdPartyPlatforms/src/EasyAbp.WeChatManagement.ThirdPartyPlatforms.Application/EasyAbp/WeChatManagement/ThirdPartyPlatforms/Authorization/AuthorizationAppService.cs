using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.ApiRequests;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AuthorizerRefreshToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services.Response;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Caches;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Models;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Security.Encryption;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization;

public class AuthorizationAppService : ThirdPartyPlatformsAppService, IAuthorizationAppService
{
    private readonly IDistributedEventBus _distributedEventBus;
    private readonly IAbpWeChatServiceFactory _abpWeChatServiceFactory;
    private readonly IWeChatAppRepository _weChatAppRepository;
    private readonly IAuthorizerSecretRepository _authorizerSecretRepository;
    private readonly IAuthorizerAccessTokenCache _authorizerAccessTokenCache;
    private readonly IAuthorizerRefreshTokenStore _authorizerRefreshTokenStore;
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IDistributedCache<WeChatThirdPartyPlatformPreAuthCacheItem> _cache;

    public AuthorizationAppService(
        IDistributedEventBus distributedEventBus,
        IAbpWeChatServiceFactory abpWeChatServiceFactory,
        IWeChatAppRepository weChatAppRepository,
        IAuthorizerSecretRepository authorizerSecretRepository,
        IAuthorizerAccessTokenCache authorizerAccessTokenCache,
        IAuthorizerRefreshTokenStore authorizerRefreshTokenStore,
        IStringEncryptionService stringEncryptionService,
        IDistributedCache<WeChatThirdPartyPlatformPreAuthCacheItem> cache)
    {
        _distributedEventBus = distributedEventBus;
        _abpWeChatServiceFactory = abpWeChatServiceFactory;
        _weChatAppRepository = weChatAppRepository;
        _authorizerSecretRepository = authorizerSecretRepository;
        _authorizerAccessTokenCache = authorizerAccessTokenCache;
        _authorizerRefreshTokenStore = authorizerRefreshTokenStore;
        _stringEncryptionService = stringEncryptionService;
        _cache = cache;
    }

    [Authorize(ThirdPartyPlatformsPermissions.Authorization.CreateRequest)]
    public virtual async Task<PreAuthResultDto> PreAuthAsync(PreAuthInputDto input)
    {
        var thirdPartyPlatformWeChatApp =
            await _weChatAppRepository.GetThirdPartyPlatformAppAsync(input.ThirdPartyPlatformWeChatAppId);

        var thirdPartyPlatformApiService =
            await _abpWeChatServiceFactory.CreateAsync<ThirdPartyPlatformWeService>(thirdPartyPlatformWeChatApp.AppId);

        var response = await thirdPartyPlatformApiService.GetPreAuthCodeAsync();

        if (response.ErrorCode != 0 || response.PreAuthCode.IsNullOrWhiteSpace())
        {
            throw new UserFriendlyException(
                $"微信 api_create_preauthcode 接口调用失败。错误代码：{response.ErrorCode}，错误信息：{response.ErrorMessage}");
        }

        var token = Guid.NewGuid().ToString();

        await _cache.SetAsync(token, new WeChatThirdPartyPlatformPreAuthCacheItem
        {
            ThirdPartyPlatformWeChatAppId = thirdPartyPlatformWeChatApp.Id,
            AuthorizerName = input.AuthorizerName,
            AllowOfficial = input.AllowOfficial,
            AllowMiniProgram = input.AllowMiniProgram,
            SpecifiedAppId = input.SpecifiedAppId,
            CategoryIds = input.CategoryIds
        }, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(600)
        });

        return new PreAuthResultDto
        {
            PreAuthCode = response.PreAuthCode,
            Token = token
        };
    }

    public virtual async Task<HandleCallbackResultDto> HandleCallbackAsync(HandleCallbackInputDto input)
    {
        var cacheItem = await _cache.GetAsync(input.Token);

        if (cacheItem is null)
        {
            return new HandleCallbackResultDto
            {
                ErrorCode = -1,
                ErrorMessage = $"不存在的授权 Token：{input.Token}，请联系管理员。"
            };
        }

        await _cache.RemoveAsync(input.Token);

        var thirdPartyPlatformWeChatApp = await _weChatAppRepository.FindAsync(cacheItem.ThirdPartyPlatformWeChatAppId);

        if (thirdPartyPlatformWeChatApp is null)
        {
            return new HandleCallbackResultDto
            {
                ErrorCode = -1,
                ErrorMessage = $"不存在的第三方平台：{cacheItem.ThirdPartyPlatformWeChatAppId}"
            };
        }

        var thirdPartyPlatformApiService =
            await _abpWeChatServiceFactory.CreateAsync<ThirdPartyPlatformWeService>(thirdPartyPlatformWeChatApp.AppId);

        var response = await thirdPartyPlatformApiService.QueryAuthAsync(input.AuthorizationCode);

        if (response.ErrorCode != 0)
        {
            Logger.LogError(
                "第三方平台授权失败。第三方平台：{ThirdPartyPlatformWeChatAppId}。授权方：{authorizerName}。",
                cacheItem.ThirdPartyPlatformWeChatAppId, cacheItem.AuthorizerName);

            return new HandleCallbackResultDto
            {
                ErrorCode = response.ErrorCode,
                ErrorMessage = response.ErrorMessage
            };
        }

        var authorizerWeChatApp =
            await _weChatAppRepository.FindAsync(x => x.AppId == response.AuthorizationInfo.AuthorizerAppId);

        if (authorizerWeChatApp == null)
        {
            authorizerWeChatApp =
                await CreateAuthorizerWeChatAppAsync(thirdPartyPlatformWeChatApp, response, cacheItem.AuthorizerName);
        }
        else if (authorizerWeChatApp.OpenAppIdOrName != await GenerateOpenAppIdOrNameAsync(cacheItem.AuthorizerName))
        {
            return new HandleCallbackResultDto
            {
                ErrorCode = -1,
                ErrorMessage = $"授权方名称：{cacheItem.AuthorizerName} 与登记的名称不符，请联系管理员调整"
            };
        }

        await CreateOrUpdateAuthorizerSecretAsync(thirdPartyPlatformWeChatApp, response);

        await _distributedEventBus.PublishAsync(new ThirdPartyPlatformAuthorizedEto
        {
            TenantId = CurrentTenant.Id,
            ThirdPartyPlatformWeChatAppId = thirdPartyPlatformWeChatApp.Id,
            AuthorizerWeChatAppId = authorizerWeChatApp.Id,
            ComponentAppId = thirdPartyPlatformWeChatApp.AppId,
            AuthorizerAppId = authorizerWeChatApp.AppId,
            AuthorizerName = cacheItem.AuthorizerName,
            CategoryIds = cacheItem.CategoryIds
        });

        Logger.LogInformation(
            "第三方平台授权成功。第三方平台：{ThirdPartyPlatformWeChatAppId}。授权方：{authorizerName}。授权应用：{AuthorizerAppId}。",
            cacheItem.ThirdPartyPlatformWeChatAppId, cacheItem.AuthorizerName,
            response.AuthorizationInfo.AuthorizerAppId);

        return new HandleCallbackResultDto
        {
            ErrorCode = response.ErrorCode,
            ErrorMessage = response.ErrorMessage
        };
    }

    protected async Task<WeChatApp> CreateAuthorizerWeChatAppAsync(WeChatApp thirdPartyPlatformWeChatApp,
        QueryAuthResponse response, string authorizerName)
    {
        var appInfo = await QueryAuthorizerWeChatAppInfoAsync(thirdPartyPlatformWeChatApp,
            response.AuthorizationInfo.AuthorizerAppId);

        var authorizerWeChatApp = new WeChatApp(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            appInfo.WeChatAppType,
            thirdPartyPlatformWeChatApp.Id,
            appInfo.Username,
            appInfo.Nickname,
            await GenerateOpenAppIdOrNameAsync(authorizerName),
            response.AuthorizationInfo.AuthorizerAppId,
            null,
            null,
            null,
            false);

        return await _weChatAppRepository.InsertAsync(authorizerWeChatApp, true);
    }

    protected virtual Task<string> GenerateOpenAppIdOrNameAsync(string authorizerName)
    {
        return Task.FromResult($"3rd-party:{authorizerName}");
    }

    protected virtual async Task<AuthorizerInfoModel> QueryAuthorizerWeChatAppInfoAsync(
        WeChatApp thirdPartyPlatformWeChatApp, string authorizerAppId)
    {
        var requester = LazyServiceProvider.LazyGetRequiredService<IWeChatThirdPartyPlatformApiRequester>();
        var optionsProvider = LazyServiceProvider
            .LazyGetRequiredService<IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions>>();

        var response = await requester.RequestAsync(
            "https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_info",
            HttpMethod.Post,
            new GetAuthorizerInfoRequest
            {
                ComponentAppId = thirdPartyPlatformWeChatApp.AppId,
                AuthorizerAppId = authorizerAppId
            },
            await optionsProvider.GetAsync(thirdPartyPlatformWeChatApp.AppId)
        );

        var jObject = JObject.Parse(response);
        var authorizerInfo = jObject.SelectToken("authorizer_info");

        if (authorizerInfo is null)
        {
            Logger.LogWarning("api_get_authorizer_info 预期外的返回内容：{response}", response);

            throw new UserFriendlyException("无法通过 api_get_authorizer_info 接口获取微信应用信息，授权处理失败");
        }

        return new AuthorizerInfoModel
        {
            Username = authorizerInfo.SelectToken("user_name")?.Value<string>() ?? $"UnknownApp{Random.Shared.Next()}",
            Nickname = authorizerInfo.SelectToken("nick_name")?.Value<string>() ?? $"未知应用{Random.Shared.Next()}",
            WeChatAppType = authorizerInfo.SelectToken("MiniProgramInfo") is null
                ? WeChatAppType.Official
                : WeChatAppType.MiniProgram,
            RawData = response
        };
    }

    protected virtual async Task CreateOrUpdateAuthorizerSecretAsync(WeChatApp thirdPartyPlatformWeChatApp,
        QueryAuthResponse response)
    {
        var authorizerSecret = await _authorizerSecretRepository.FindAsync(x =>
            x.ComponentAppId == thirdPartyPlatformWeChatApp.AppId &&
            x.AuthorizerAppId == response.AuthorizationInfo.AuthorizerAppId);

        var categoryIds = response.AuthorizationInfo.FuncInfo.Select(x => x.FuncScopeCategory.Id);
        var encryptedRefreshToken = _stringEncryptionService.Encrypt(response.AuthorizationInfo.AuthorizerRefreshToken);

        if (authorizerSecret is null)
        {
            await _authorizerSecretRepository.InsertAsync(new AuthorizerSecret(
                GuidGenerator.Create(),
                CurrentTenant.Id,
                thirdPartyPlatformWeChatApp.AppId,
                response.AuthorizationInfo.AuthorizerAppId,
                encryptedRefreshToken,
                categoryIds.ToList()), true);
        }
        else
        {
            authorizerSecret.SetEncryptedRefreshToken(encryptedRefreshToken);
            authorizerSecret.CategoryIds.Clear();
            authorizerSecret.CategoryIds.AddRange(categoryIds);

            await _authorizerSecretRepository.UpdateAsync(authorizerSecret, true);
        }

        await _authorizerRefreshTokenStore.SetAsync(thirdPartyPlatformWeChatApp.AppId,
            response.AuthorizationInfo.AuthorizerAppId, response.AuthorizationInfo.AuthorizerRefreshToken);

        await _authorizerAccessTokenCache.SetAsync(thirdPartyPlatformWeChatApp.AppId,
            response.AuthorizationInfo.AuthorizerAppId, response.AuthorizationInfo.AuthorizerAccessToken);
    }
}