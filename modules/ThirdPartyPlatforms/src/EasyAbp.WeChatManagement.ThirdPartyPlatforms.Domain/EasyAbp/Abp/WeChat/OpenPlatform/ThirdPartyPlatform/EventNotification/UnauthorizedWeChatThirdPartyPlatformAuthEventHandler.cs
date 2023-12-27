using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.EventNotification;

public class UnauthorizedWeChatThirdPartyPlatformAuthEventHandler :
    WeChatThirdPartyPlatformAuthEventHandlerBase<UnauthorizedWeChatThirdPartyPlatformAuthEventHandler>,
    ITransientDependency
{
    public override string InfoType => WeChatThirdPartyPlatformAuthEventInfoTypes.Unauthorized;

    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IAuthorizerAccessTokenCache _authorizerAccessTokenCache;
    private readonly ILogger<UnauthorizedWeChatThirdPartyPlatformAuthEventHandler> _logger;
    private readonly IAuthorizerSecretRepository _authorizerSecretRepository;

    public UnauthorizedWeChatThirdPartyPlatformAuthEventHandler(
        IUnitOfWorkManager unitOfWorkManager,
        IAuthorizerAccessTokenCache authorizerAccessTokenCache,
        ILogger<UnauthorizedWeChatThirdPartyPlatformAuthEventHandler> logger,
        IAuthorizerSecretRepository authorizerSecretRepository)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _authorizerAccessTokenCache = authorizerAccessTokenCache;
        _logger = logger;
        _authorizerSecretRepository = authorizerSecretRepository;
    }

    public override async Task<WeChatRequestHandlingResult> HandleAsync(AuthEventModel model)
    {
        using var uow = _unitOfWorkManager.Begin(true);

        var authorizerSecret = await _authorizerSecretRepository.FindAsync(x =>
            x.ComponentAppId == model.AppId && x.AuthorizerAppId == model.AuthorizerAppId);

        if (authorizerSecret is null)
        {
            _logger.LogInformation(
                "实际没有删除 AuthorizerSecret (ComponentAppId={ComponentAppId}, AuthorizerAppId={AuthorizerAppId}) 实体，因为实体不存在",
                model.AppId, model.AuthorizerAppId);

            return new WeChatRequestHandlingResult(true);
        }

        try
        {
            await _authorizerSecretRepository.DeleteAsync(authorizerSecret, true);
            await uow.CompleteAsync();
        }
        catch
        {
            _logger.LogWarning(
                "由于 UOW 提交不成功，导致删除 AuthorizerSecret (ComponentAppId={ComponentAppId}, AuthorizerAppId={AuthorizerAppId}) 实体失败",
                model.AppId, model.AuthorizerAppId);

            return new WeChatRequestHandlingResult(false);
        }

        try
        {
            await _authorizerAccessTokenCache.SetAsync(model.AppId, model.AuthorizerAppId, null);
        }
        catch (Exception e)
        {
            _logger.LogWarning("写入 AuthorizerAccessTokenCache 失败");
            _logger.LogException(e);
        }

        return new WeChatRequestHandlingResult(true);
    }
}