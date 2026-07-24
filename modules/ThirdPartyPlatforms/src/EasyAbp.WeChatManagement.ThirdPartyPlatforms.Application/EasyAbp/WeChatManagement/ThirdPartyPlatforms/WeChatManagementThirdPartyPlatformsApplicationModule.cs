using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;
using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.RequestHandling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(WeChatManagementCommonApplicationModule),
    typeof(WeChatManagementThirdPartyPlatformsDomainModule),
    typeof(WeChatManagementThirdPartyPlatformsApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule)
)]
public class WeChatManagementThirdPartyPlatformsApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<WeChatManagementThirdPartyPlatformsApplicationModule>();

        context.Services.AddTransient<IWeChatThirdPartyPlatformEventRequestHandlingService, EventHandlingAppService>();
    }
}