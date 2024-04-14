using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;
using EasyAbp.WeChatManagement.Common;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Uow;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.RequestHandling;

[RemoteService(Name = WeChatManagementRemoteServiceConsts.RemoteServiceName)]
[Route("/api/wechat-management/third-party-platforms/event-handling")]
[UnitOfWork(IsDisabled = true)]
public class EventHandlingController : ThirdPartyPlatformsController, IEventHandlingAppService
{
    private readonly IEventHandlingAppService _service;

    public EventHandlingController(IEventHandlingAppService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("notify/auth")]
    public virtual Task<WeChatRequestHandlingResult> NotifyAuthAsync(NotifyAuthInput input)
    {
        return _service.NotifyAuthAsync(input);
    }

    [HttpPost]
    [Route("notify/app")]
    public virtual Task<AppEventHandlingResult> NotifyAppAsync(NotifyAppInput input)
    {
        return _service.NotifyAppAsync(input);
    }
}