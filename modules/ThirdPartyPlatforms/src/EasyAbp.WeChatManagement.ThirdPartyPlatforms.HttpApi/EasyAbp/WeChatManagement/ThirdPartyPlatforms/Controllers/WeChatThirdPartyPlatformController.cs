using System;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
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
    protected IAuthorizationAppService AuthorizationAppService { get; }

    public WeChatThirdPartyPlatformController(
        IAuthCallbackActionResultProvider authCallbackActionResultProvider,
        IAuthorizationAppService authorizationAppService)
    {
        AuthCallbackActionResultProvider = authCallbackActionResultProvider;
        AuthorizationAppService = authorizationAppService;
    }

    /// <summary>
    /// 授权成功回调
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Before_Develop/Authorization_Process_Technical_Description.html
    /// </summary>
    /// <param name="tenantId">租户 Id</param>
    /// <param name="token">微信管理模块生成的 token</param>
    /// <returns></returns>
    [HttpGet]
    [Route("auth-callback/token/{token}")]
    public virtual async Task<ActionResult> AuthCallbackAsync(string tenantId, string token)
    {
        using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId));

        var result =
            await AuthorizationAppService.HandleCallbackAsync(new HandleCallbackInputDto(
                Request.Query["auth_code"].First(), token));

        if (result.ErrorCode == 0)
        {
            Logger.LogInformation("第三方平台授权成功。Token：{weChatAppId}", token);
        }
        else
        {
            Logger.LogError("第三方平台授权失败。Token：{weChatAppId}", token);
        }

        return await AuthCallbackActionResultProvider.GetAsync(result);
    }
    
    /// <summary>
    /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
    /// 见 <see cref="AuthCallbackAsync"/>
    /// </summary>
    [HttpGet]
    [Route("auth-callback/tenant-id/{tenantId}/token/{token}")]
    public virtual Task<ActionResult> AuthCallback2Async(string tenantId, string token)
    {
        return AuthCallbackAsync(tenantId, token);
    }
}