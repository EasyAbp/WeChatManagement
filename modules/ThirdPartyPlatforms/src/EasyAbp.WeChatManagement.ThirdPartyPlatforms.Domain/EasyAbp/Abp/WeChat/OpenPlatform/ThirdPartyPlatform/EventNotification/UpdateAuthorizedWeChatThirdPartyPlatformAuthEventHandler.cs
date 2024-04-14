using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Uow;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.EventNotification;

public class UpdateAuthorizedWeChatThirdPartyPlatformAuthEventHandler :
    WeChatThirdPartyPlatformAuthEventHandlerBase<UpdateAuthorizedWeChatThirdPartyPlatformAuthEventHandler>,
    ITransientDependency
{
    public override string InfoType => WeChatThirdPartyPlatformAuthEventInfoTypes.UpdateAuthorized;

    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IAuthorizerAccessTokenCache _authorizerAccessTokenCache;
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly ILogger<UpdateAuthorizedWeChatThirdPartyPlatformAuthEventHandler> _logger;
    private readonly IAuthorizerSecretRepository _authorizerSecretRepository;
    private readonly IAbpWeChatServiceFactory _abpWeChatServiceFactory;

    public UpdateAuthorizedWeChatThirdPartyPlatformAuthEventHandler(
        IUnitOfWorkManager unitOfWorkManager,
        IAuthorizerAccessTokenCache authorizerAccessTokenCache,
        IStringEncryptionService stringEncryptionService,
        ILogger<UpdateAuthorizedWeChatThirdPartyPlatformAuthEventHandler> logger,
        IAuthorizerSecretRepository authorizerSecretRepository,
        IAbpWeChatServiceFactory abpWeChatServiceFactory)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _authorizerAccessTokenCache = authorizerAccessTokenCache;
        _stringEncryptionService = stringEncryptionService;
        _logger = logger;
        _authorizerSecretRepository = authorizerSecretRepository;
        _abpWeChatServiceFactory = abpWeChatServiceFactory;
    }

    public override async Task<WeChatRequestHandlingResult> HandleAsync(AuthEventModel model)
    {
        using var uow = _unitOfWorkManager.Begin(true);

        var authorizerSecret = await _authorizerSecretRepository.FindAsync(x =>
            x.ComponentAppId == model.AppId && x.AuthorizerAppId == model.AuthorizerAppId);

        if (authorizerSecret is null)
        {
            _logger.LogWarning(
                "由于 AuthorizerSecret (ComponentAppId={ComponentAppId}, AuthorizerAppId={AuthorizerAppId}) 实体不存在，更新 RefreshToken 失败",
                model.AppId, model.AuthorizerAppId);

            return new WeChatRequestHandlingResult(false);
        }

        var thirdPartyPlatformWeService =
            await _abpWeChatServiceFactory.CreateAsync<ThirdPartyPlatformWeService>(model.AppId);

        var response = await thirdPartyPlatformWeService.QueryAuthAsync(model.AuthorizationCode);

        if (response.ErrorCode != 0)
        {
            _logger.LogWarning(
                "微信第三方平台查询授权获取 refresh_token 失败。ComponentAppId={ComponentAppId}, AuthorizerAppId={AuthorizerAppId}, 错误码：{ErrorCode}，错误信息：{ErrorMessage}",
                model.AppId, model.AuthorizerAppId, response.ErrorCode, response.ErrorMessage);

            return new WeChatRequestHandlingResult(false);
        }

        var categoryIds = response.AuthorizationInfo.FuncInfo.Select(x => x.FuncScopeCategory.Id);

        var encryptedRefreshToken = _stringEncryptionService.Encrypt(response.AuthorizationInfo.AuthorizerRefreshToken);

        authorizerSecret.SetEncryptedRefreshToken(encryptedRefreshToken);
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
                "由于 UOW 提交不成功，导致更新 AuthorizerSecret (ComponentAppId={ComponentAppId}, AuthorizerAppId={AuthorizerAppId}) 实体的 RefreshToken 失败",
                model.AppId, model.AuthorizerAppId);

            return new WeChatRequestHandlingResult(false);
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

        return new WeChatRequestHandlingResult(true);
    }
}