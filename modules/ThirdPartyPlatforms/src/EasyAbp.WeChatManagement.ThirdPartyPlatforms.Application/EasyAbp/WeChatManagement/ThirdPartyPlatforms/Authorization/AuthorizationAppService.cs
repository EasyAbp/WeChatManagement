using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.OpenPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.AuthorizerRefreshToken;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.ComponentAccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving.Contributors;
using EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform.Response;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Caches;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Models;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Security.Encryption;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization;

public class AuthorizationAppService : ApplicationService, IAuthorizationAppService
{
    private readonly IWeChatAppRepository _weChatAppRepository;
    private readonly IAuthorizerSecretRepository _authorizerSecretRepository;
    private readonly IAuthorizerAccessTokenCache _authorizerAccessTokenCache;
    private readonly IAuthorizerRefreshTokenStore _authorizerRefreshTokenStore;
    private readonly IWeChatThirdPartyPlatformAsyncLocal _weChatThirdPartyPlatformAsyncLocal;
    private readonly ThirdPartyPlatformApiService _thirdPartyPlatformApiService;
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IDistributedCache<WeChatThirdPartyPlatformPreAuthCacheItem> _cache;

    public AuthorizationAppService(
        IWeChatAppRepository weChatAppRepository,
        IAuthorizerSecretRepository authorizerSecretRepository,
        IAuthorizerAccessTokenCache authorizerAccessTokenCache,
        IAuthorizerRefreshTokenStore authorizerRefreshTokenStore,
        IWeChatThirdPartyPlatformAsyncLocal weChatThirdPartyPlatformAsyncLocal,
        ThirdPartyPlatformApiService thirdPartyPlatformApiService,
        IStringEncryptionService stringEncryptionService,
        IDistributedCache<WeChatThirdPartyPlatformPreAuthCacheItem> cache)
    {
        _weChatAppRepository = weChatAppRepository;
        _authorizerSecretRepository = authorizerSecretRepository;
        _authorizerAccessTokenCache = authorizerAccessTokenCache;
        _authorizerRefreshTokenStore = authorizerRefreshTokenStore;
        _weChatThirdPartyPlatformAsyncLocal = weChatThirdPartyPlatformAsyncLocal;
        _thirdPartyPlatformApiService = thirdPartyPlatformApiService;
        _stringEncryptionService = stringEncryptionService;
        _cache = cache;
    }

    public virtual async Task<PreAuthResultDto> PreAuthAsync(PreAuthInputDto input)
    {
        return new PreAuthResultDto
        {
            PreAuthCode = "123"
        };
        var thirdPartyPlatformWeChatApp =
            await _weChatAppRepository.GetThirdPartyPlatformAppAsync(input.ThirdPartyPlatformWeChatAppId);

        using var changeOptions = _weChatThirdPartyPlatformAsyncLocal.Change(new AbpWeChatThirdPartyPlatformOptions
        {
            Token = thirdPartyPlatformWeChatApp.Token,
            AppId = thirdPartyPlatformWeChatApp.AppId,
            AppSecret = thirdPartyPlatformWeChatApp.AppSecret,
            EncodingAesKey = thirdPartyPlatformWeChatApp.EncodingAesKey
        });

        var response = await _thirdPartyPlatformApiService.GetPreAuthCodeAsync();

        if (response.ErrorCode != 0 || response.PreAuthCode.IsNullOrWhiteSpace())
        {
            throw new UserFriendlyException(
                $"微信 api_create_preauthcode 接口调用失败。错误代码：{response.ErrorCode}，错误信息：{response.ErrorMessage}");
        }

        await _cache.SetAsync(response.PreAuthCode, new WeChatThirdPartyPlatformPreAuthCacheItem
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
            PreAuthCode = response.PreAuthCode
        };
    }

    public virtual async Task<HandleCallbackResultDto> HandleCallbackAsync(HandleCallbackInputDto input)
    {
        var cacheItem = await _cache.GetAsync(input.PreAuthCode);

        if (cacheItem is null)
        {
            return new HandleCallbackResultDto
            {
                ErrorCode = -1,
                ErrorMessage = $"不存在的预授权 {input.PreAuthCode}，请联系管理员。"
            };
        }

        var thirdPartyPlatformWeChatApp = await _weChatAppRepository.FindAsync(input.ThirdPartyPlatformWeChatAppId);

        if (thirdPartyPlatformWeChatApp is null)
        {
            return new HandleCallbackResultDto
            {
                ErrorCode = -1,
                ErrorMessage = $"不存在的第三方平台：{input.ThirdPartyPlatformWeChatAppId}"
            };
        }

        using var changeOptions = _weChatThirdPartyPlatformAsyncLocal.Change(new AbpWeChatThirdPartyPlatformOptions
        {
            Token = thirdPartyPlatformWeChatApp.Token,
            AppId = thirdPartyPlatformWeChatApp.AppId,
            AppSecret = thirdPartyPlatformWeChatApp.AppSecret,
            EncodingAesKey = thirdPartyPlatformWeChatApp.EncodingAesKey
        });

        var response = await _thirdPartyPlatformApiService.QueryAuthAsync(input.AuthorizationCode);

        if (await _weChatAppRepository.FindAsync(x => x.AppId == response.AuthorizationInfo.AuthorizerAppId) == null)
        {
            await CreateAuthorizerWeChatAppAsync(thirdPartyPlatformWeChatApp, response, cacheItem.AuthorizerName);
        }

        await CreateOrUpdateAuthorizerSecretAsync(thirdPartyPlatformWeChatApp, response);

        return new HandleCallbackResultDto
        {
            ErrorCode = response.ErrorCode,
            ErrorMessage = response.ErrorMessage
        };
    }

    protected async Task CreateAuthorizerWeChatAppAsync(WeChatApp thirdPartyPlatformWeChatApp,
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

        await _weChatAppRepository.InsertAsync(authorizerWeChatApp, true);
    }

    protected virtual async Task<string> GenerateOpenAppIdOrNameAsync(string authorizerName)
    {
        return $"3rd-party:{authorizerName}";
    }

    protected virtual async Task<AuthorizerInfoModel> QueryAuthorizerWeChatAppInfoAsync(
        WeChatApp thirdPartyPlatformWeChatApp, string authorizerAppId)
    {
        var requester = LazyServiceProvider.LazyGetRequiredService<IWeChatOpenPlatformApiRequester>();
        var componentAccessTokenProvider = LazyServiceProvider.LazyGetRequiredService<IComponentAccessTokenProvider>();

        var componentAccessToken = await componentAccessTokenProvider.GetAsync(
            thirdPartyPlatformWeChatApp.AppId, thirdPartyPlatformWeChatApp.AppSecret);

        var url = $"https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_info?" +
                  $"component_access_token={componentAccessToken}";

        var response = await requester.RequestAsync(url, HttpMethod.Post, new GetAuthorizerInfoRequest
        {
            ComponentAppId = thirdPartyPlatformWeChatApp.AppId,
            AuthorizerAppId = authorizerAppId
        });

        var jObject = JObject.Parse(response);
        var authorizerInfo = jObject.SelectToken("authorizer_info");

        if (authorizerInfo is null)
        {
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
            authorizerSecret.SetRefreshToken(encryptedRefreshToken, _stringEncryptionService);
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