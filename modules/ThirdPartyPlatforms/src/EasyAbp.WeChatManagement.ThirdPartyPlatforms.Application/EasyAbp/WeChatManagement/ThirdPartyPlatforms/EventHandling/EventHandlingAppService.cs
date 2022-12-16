using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.EventHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.EventHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.EventNotification;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EventHandling;

[UnitOfWork(IsDisabled = true)]
public class EventHandlingAppService : ApplicationService, IEventHandlingAppService
{
    private readonly WeChatThirdPartyPlatformEventHandlingService _weChatThirdPartyPlatformEventHandlingService;

    public EventHandlingAppService(
        WeChatThirdPartyPlatformEventHandlingService weChatThirdPartyPlatformEventHandlingService)
    {
        _weChatThirdPartyPlatformEventHandlingService = weChatThirdPartyPlatformEventHandlingService;
    }

    public virtual Task<WeChatEventHandlingResult> NotifyAuthAsync(string componentAppId,
        WeChatEventNotificationRequestModel request)
    {
        return _weChatThirdPartyPlatformEventHandlingService.NotifyAuthAsync(componentAppId, request);
    }

    public virtual Task<WeChatEventHandlingResult> NotifyAppAsync(string componentAppId, string appId,
        WeChatEventNotificationRequestModel request)
    {
        return _weChatThirdPartyPlatformEventHandlingService.NotifyAppAsync(componentAppId, appId, request);
    }
}