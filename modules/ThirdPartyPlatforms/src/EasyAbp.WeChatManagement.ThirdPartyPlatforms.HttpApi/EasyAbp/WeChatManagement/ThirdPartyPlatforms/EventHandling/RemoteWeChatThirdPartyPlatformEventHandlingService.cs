using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.EventHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.EventHandling;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EventHandling;

[Dependency(TryRegister = true)]
public class RemoteWeChatThirdPartyPlatformEventHandlingService :
    IWeChatThirdPartyPlatformEventHandlingService, ITransientDependency
{
    private readonly IEventHandlingAppService _eventHandlingAppService;

    public RemoteWeChatThirdPartyPlatformEventHandlingService(IEventHandlingAppService eventHandlingAppService)
    {
        _eventHandlingAppService = eventHandlingAppService;
    }

    public virtual Task<WeChatEventHandlingResult> NotifyAuthAsync(string componentAppId,
        WeChatEventNotificationRequestModel request)
    {
        return _eventHandlingAppService.NotifyAuthAsync(componentAppId, request);
    }

    public virtual Task<WeChatEventHandlingResult> NotifyAppAsync(string componentAppId, string authorizerAppId,
        WeChatEventNotificationRequestModel request)
    {
        return _eventHandlingAppService.NotifyAppAsync(componentAppId, authorizerAppId, request);
    }
}