using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.RequestHandling;

[Dependency(TryRegister = true)]
public class RemoteWeChatThirdPartyPlatformEventHandlingService :
    IWeChatThirdPartyPlatformEventRequestHandlingService, ITransientDependency
{
    private readonly IEventHandlingAppService _eventHandlingAppService;

    public RemoteWeChatThirdPartyPlatformEventHandlingService(IEventHandlingAppService eventHandlingAppService)
    {
        _eventHandlingAppService = eventHandlingAppService;
    }

    public virtual Task<WeChatRequestHandlingResult> NotifyAuthAsync(
        string componentAppId, WeChatEventRequestModel request)
    {
        return _eventHandlingAppService.NotifyAuthAsync(componentAppId, request);
    }

    public virtual Task<WeChatRequestHandlingResult> NotifyAppAsync(
        string componentAppId, string authorizerAppId, WeChatEventRequestModel request)
    {
        return _eventHandlingAppService.NotifyAppAsync(componentAppId, authorizerAppId, request);
    }
}