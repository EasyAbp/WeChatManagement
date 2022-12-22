using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.OpenPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(AbpWeChatOpenPlatformModule),
    typeof(WeChatManagementCommonDomainModule),
    typeof(AbpDddDomainModule),
    typeof(WeChatManagementThirdPartyPlatformsDomainSharedModule)
)]
public class WeChatManagementThirdPartyPlatformsDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services
            .AddTransient<IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions>,
                WeChatManagementThirdPartyPlatformAbpWeChatOptionsProvider>();
    }
}