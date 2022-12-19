using EasyAbp.Abp.WeChat.OpenPlatform;
using EasyAbp.WeChatManagement.Common;
using Localization.Resources.AbpUi;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(WeChatManagementCommonHttpApiModule),
    typeof(WeChatManagementThirdPartyPlatformsApplicationContractsModule),
    typeof(AbpWeChatOpenPlatformHttpApiModule),
    typeof(AbpAspNetCoreMvcModule))]
public class WeChatManagementThirdPartyPlatformsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(WeChatManagementThirdPartyPlatformsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ThirdPartyPlatformsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
