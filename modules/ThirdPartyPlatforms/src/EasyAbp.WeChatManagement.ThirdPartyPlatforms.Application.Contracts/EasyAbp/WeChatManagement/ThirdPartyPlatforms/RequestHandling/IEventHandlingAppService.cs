using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.RequestHandling;

public interface IEventHandlingAppService : IApplicationService, IWeChatThirdPartyPlatformEventRequestHandlingService
{
}