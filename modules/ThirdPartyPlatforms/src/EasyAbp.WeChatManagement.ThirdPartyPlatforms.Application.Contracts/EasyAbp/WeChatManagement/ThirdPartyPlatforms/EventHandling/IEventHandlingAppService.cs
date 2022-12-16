using EasyAbp.Abp.WeChat.OpenPlatform.EventHandling;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EventHandling;

public interface IEventHandlingAppService : IApplicationService, IWeChatThirdPartyPlatformEventHandlingService
{
}