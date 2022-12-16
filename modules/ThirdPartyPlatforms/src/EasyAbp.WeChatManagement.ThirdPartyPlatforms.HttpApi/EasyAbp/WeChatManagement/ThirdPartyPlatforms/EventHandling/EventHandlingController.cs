using EasyAbp.Abp.WeChat.OpenPlatform.EventHandling;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.EventHandling;
using EasyAbp.WeChatManagement.Common;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Uow;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EventHandling;

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

    public virtual Task<WeChatEventHandlingResult> NotifyAuthAsync(string componentAppId,
        WeChatEventNotificationRequestModel request)
    {
        return _service.NotifyAuthAsync(componentAppId, request);
    }

    public virtual Task<WeChatEventHandlingResult> NotifyAppAsync(string componentAppId, string authorizerAppId,
        WeChatEventNotificationRequestModel request)
    {
        return _service.NotifyAppAsync(componentAppId, authorizerAppId, request);
    }
}