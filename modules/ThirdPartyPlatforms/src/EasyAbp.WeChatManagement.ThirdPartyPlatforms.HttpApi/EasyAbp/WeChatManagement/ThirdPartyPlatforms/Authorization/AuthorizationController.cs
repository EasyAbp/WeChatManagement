using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization;

[RemoteService(Name = WeChatManagementRemoteServiceConsts.RemoteServiceName)]
[Route("/api/wechat-management/third-party-platforms/authorization")]
public class AuthorizationController : ThirdPartyPlatformsController, IAuthorizationAppService
{
    private readonly IAuthorizationAppService _service;

    public AuthorizationController(IAuthorizationAppService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("pre-auth")]
    public virtual Task<PreAuthResultDto> PreAuthAsync(PreAuthInputDto input)
    {
        return _service.PreAuthAsync(input);
    }

    [HttpPost]
    [Route("handle-callback")]
    public virtual Task<HandleCallbackResultDto> HandleCallbackAsync(HandleCallbackInputDto input)
    {
        return _service.HandleCallbackAsync(input);
    }
}