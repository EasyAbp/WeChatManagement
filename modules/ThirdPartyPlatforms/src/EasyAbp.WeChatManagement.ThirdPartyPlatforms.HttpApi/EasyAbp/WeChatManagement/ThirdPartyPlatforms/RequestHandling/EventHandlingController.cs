using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
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

    public virtual Task<WeChatRequestHandlingResult> NotifyAuthAsync(
        string componentAppId, WeChatEventRequestModel request)
    {
        return _service.NotifyAuthAsync(componentAppId, request);
    }

    public virtual Task<WeChatRequestHandlingResult> NotifyAppAsync(
        string componentAppId, string authorizerAppId, WeChatEventRequestModel request)
    {
        return _service.NotifyAppAsync(componentAppId, authorizerAppId, request);
    }
}