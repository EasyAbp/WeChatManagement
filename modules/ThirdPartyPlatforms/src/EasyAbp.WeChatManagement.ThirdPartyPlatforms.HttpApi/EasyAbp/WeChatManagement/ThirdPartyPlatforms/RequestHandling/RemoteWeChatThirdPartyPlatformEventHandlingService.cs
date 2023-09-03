using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;
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

    public virtual Task<WeChatRequestHandlingResult> NotifyAuthAsync(NotifyAuthInput input)
    {
        return _eventHandlingAppService.NotifyAuthAsync(input);
    }

    public virtual Task<AppEventHandlingResult> NotifyAppAsync(NotifyAppInput input)
    {
        return _eventHandlingAppService.NotifyAppAsync(input);
    }
}