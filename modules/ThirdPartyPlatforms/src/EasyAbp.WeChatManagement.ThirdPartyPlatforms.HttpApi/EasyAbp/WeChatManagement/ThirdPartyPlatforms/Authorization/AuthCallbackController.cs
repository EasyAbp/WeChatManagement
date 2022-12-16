using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization;

[RemoteService(Name = WeChatManagementRemoteServiceConsts.RemoteServiceName)]
[Route("/api/wechat-management/third-party-platforms/auth-callback")]
public class AuthCallbackController : ThirdPartyPlatformsController, IAuthCallbackAppService
{
    private readonly IAuthCallbackAppService _service;

    public AuthCallbackController(IAuthCallbackAppService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("handle")]
    public virtual Task<HandleAuthCallbackResultDto> HandleAsync(HandleAuthCallbackInputDto input)
    {
        return _service.HandleAsync(input);
    }
}