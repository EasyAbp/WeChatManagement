using EasyAbp.Abp.WeChat.OpenPlatform.EventHandling;
using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.EventHandling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(WeChatManagementCommonApplicationModule),
    typeof(WeChatManagementThirdPartyPlatformsDomainModule),
    typeof(WeChatManagementThirdPartyPlatformsApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
)]
public class WeChatManagementThirdPartyPlatformsApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<WeChatManagementThirdPartyPlatformsApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<WeChatManagementThirdPartyPlatformsApplicationModule>(validate: true);
        });

        context.Services.Replace(new ServiceDescriptor(typeof(IWeChatThirdPartyPlatformEventHandlingService),
            typeof(EventHandlingAppService), ServiceLifetime.Transient));
    }
}