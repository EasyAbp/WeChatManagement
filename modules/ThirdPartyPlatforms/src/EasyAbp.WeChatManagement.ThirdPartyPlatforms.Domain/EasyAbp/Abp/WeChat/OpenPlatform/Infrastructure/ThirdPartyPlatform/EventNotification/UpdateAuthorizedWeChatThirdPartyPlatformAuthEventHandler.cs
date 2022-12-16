using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.EventHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.VerifyTicket;
using EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Uow;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.EventNotification;

public class UpdateAuthorizedWeChatThirdPartyPlatformAuthEventHandler :
    IWeChatThirdPartyPlatformAuthEventHandler, ITransientDependency
{
    public string InfoType => WeChatThirdPartyPlatformAuthEventInfoTypes.UpdateAuthorized;

    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IAuthorizerAccessTokenCache _authorizerAccessTokenCache;
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly ILogger<UpdateAuthorizedWeChatThirdPartyPlatformAuthEventHandler> _logger;
    private readonly IAuthorizerSecretRepository _authorizerSecretRepository;
    private readonly ThirdPartyPlatformApiService _thirdPartyPlatformApiService;

    public UpdateAuthorizedWeChatThirdPartyPlatformAuthEventHandler(
        IUnitOfWorkManager unitOfWorkManager,
        IAuthorizerAccessTokenCache authorizerAccessTokenCache,
        IStringEncryptionService stringEncryptionService,
        ILogger<UpdateAuthorizedWeChatThirdPartyPlatformAuthEventHandler> logger,
        IAuthorizerSecretRepository authorizerSecretRepository,
        ThirdPartyPlatformApiService thirdPartyPlatformApiService)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _authorizerAccessTokenCache = authorizerAccessTokenCache;
        _stringEncryptionService = stringEncryptionService;
        _logger = logger;
        _authorizerSecretRepository = authorizerSecretRepository;
        _thirdPartyPlatformApiService = thirdPartyPlatformApiService;
    }

    public virtual async Task<WeChatEventHandlingResult> HandleAsync(AuthNotificationModel model)
    {
        using var uow = _unitOfWorkManager.Begin(true);

        var authorizerSecret = await _authorizerSecretRepository.FindAsync(x =>
            x.ComponentAppId == model.AppId && x.AuthorizerAppId == model.AuthorizerAppId);

        if (authorizerSecret is null)
        {
            _logger.LogWarning(
                "由于 AuthorizerSecret (ComponentAppId={0}, AuthorizerAppId={1}) 实体不存在，更新 RefreshToken 失败", model.AppId,
                model.AuthorizerAppId);

            return new WeChatEventHandlingResult(false);
        }

        var response = await _thirdPartyPlatformApiService.QueryAuthAsync(model.AuthorizationCode);

        if (response.ErrorCode != 0)
        {
            _logger.LogWarning(
                "微信第三方平台查询授权获取 refresh_token 失败。ComponentAppId={0}, AuthorizerAppId={1}, 错误码：{2}，错误信息：{3}",
                model.AppId, model.AuthorizerAppId, response.ErrorCode, response.ErrorMessage);

            return new WeChatEventHandlingResult(false);
        }

        var categoryIds = response.AuthorizationInfo.FuncInfo.Select(x => x.FuncScopeCategory.Id);

        authorizerSecret.SetRefreshToken(response.AuthorizationInfo.AuthorizerRefreshToken, _stringEncryptionService);
        authorizerSecret.CategoryIds.Clear();
        authorizerSecret.CategoryIds.AddRange(categoryIds);

        try
        {
            await _authorizerSecretRepository.UpdateAsync(authorizerSecret, true);
            await uow.CompleteAsync();
        }
        catch
        {
            _logger.LogWarning(
                "由于 UOW 提交不成功，导致更新 AuthorizerSecret (ComponentAppId={0}, AuthorizerAppId={1}) 实体的 RefreshToken 失败",
                model.AppId, model.AuthorizerAppId);

            return new WeChatEventHandlingResult(false);
        }

        try
        {
            await _authorizerAccessTokenCache.SetAsync(model.AppId, model.AuthorizerAppId,
                response.AuthorizationInfo.AuthorizerAccessToken);
        }
        catch (Exception e)
        {
            _logger.LogWarning("写入 AuthorizerAccessTokenCache 失败");
            _logger.LogException(e);
        }

        return new WeChatEventHandlingResult(true);
    }
}