using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.RequestHandling;

[UnitOfWork(IsDisabled = true)]
public class EventHandlingAppService : ThirdPartyPlatformsAppService, IEventHandlingAppService
{
    private readonly WeChatThirdPartyPlatformEventRequestHandlingService _handlingService;

    public EventHandlingAppService(WeChatThirdPartyPlatformEventRequestHandlingService handlingService)
    {
        _handlingService = handlingService;
    }

    public virtual Task<WeChatRequestHandlingResult> NotifyAuthAsync(NotifyAuthInput input)
    {
        return _handlingService.NotifyAuthAsync(input);
    }

    public virtual Task<AppEventHandlingResult> NotifyAppAsync(NotifyAppInput input)
    {
        return _handlingService.NotifyAppAsync(input);
    }
}