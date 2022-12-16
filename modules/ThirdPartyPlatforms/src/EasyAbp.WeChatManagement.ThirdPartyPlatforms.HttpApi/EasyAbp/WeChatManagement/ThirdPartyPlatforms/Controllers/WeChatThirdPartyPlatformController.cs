using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.ActionResultProviders;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Controllers;

[ControllerName("WeChatThirdPartyPlatform")]
[Route("/wechat/third-party-platform")]
public class WeChatThirdPartyPlatformController : ThirdPartyPlatformsController
{
    protected IAuthCallbackActionResultProvider AuthCallbackActionResultProvider { get; }
    protected IAuthCallbackAppService AuthCallbackAppService { get; }

    public WeChatThirdPartyPlatformController(
        IAuthCallbackActionResultProvider authCallbackActionResultProvider,
        IAuthCallbackAppService authCallbackAppService)
    {
        AuthCallbackActionResultProvider = authCallbackActionResultProvider;
        AuthCallbackAppService = authCallbackAppService;
    }

    /// <summary>
    /// 授权成功回调
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Before_Develop/Authorization_Process_Technical_Description.html
    /// </summary>
    /// <param name="tenantId">租户 Id</param>
    /// <param name="weChatAppId">第三方平台微信应用 Id</param>
    /// <returns></returns>
    [HttpPost]
    [Route("auth-callback/wechat-app-id/{weChatAppId:guid}/authorizer-name/{authorizerName}")]
    [Route("auth-callback/tenant-id/{tenantId}/wechat-app-id/{weChatAppId:guid}/authorizer-name/{authorizerName}")]
    public virtual async Task<ActionResult> AuthCallbackAsync(string tenantId, Guid weChatAppId, string authorizerName)
    {
        using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId));

        var result =
            await AuthCallbackAppService.HandleAsync(new HandleAuthCallbackInputDto(weChatAppId,
                HttpContext.Request.Query["auth_code"].First(), authorizerName));

        if (result.ErrorCode == 0)
        {
            Logger.LogInformation("第三方平台授权成功。WeChatAppId：{weChatAppId}", weChatAppId);
        }
        else
        {
            Logger.LogError("第三方平台授权失败。WeChatAppId：{weChatAppId}", weChatAppId);
        }

        return await AuthCallbackActionResultProvider.GetAsync(result);
    }
}