using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.RequestHandling;

[UnitOfWork(IsDisabled = true)]
public class EventHandlingAppService : ApplicationService, IEventHandlingAppService
{
    private readonly WeChatThirdPartyPlatformEventRequestHandlingService _handlingService;

    public EventHandlingAppService(WeChatThirdPartyPlatformEventRequestHandlingService handlingService)
    {
        _handlingService = handlingService;
    }

    public virtual Task<WeChatRequestHandlingResult> NotifyAuthAsync(string componentAppId,
        WeChatEventRequestModel request)
    {
        return _handlingService.NotifyAuthAsync(componentAppId, request);
    }

    public virtual Task<WeChatRequestHandlingResult> NotifyAppAsync(string componentAppId, string authorizerAppId,
        WeChatEventRequestModel request)
    {
        return _handlingService.NotifyAppAsync(componentAppId, authorizerAppId, request);
    }
}