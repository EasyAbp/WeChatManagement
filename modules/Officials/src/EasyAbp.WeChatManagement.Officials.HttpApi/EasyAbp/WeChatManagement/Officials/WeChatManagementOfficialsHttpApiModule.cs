using EasyAbp.Abp.WeChat.Official.HttpApi;
using EasyAbp.WeChatManagement.Officials.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials;

[DependsOn(
    typeof(AbpWeChatOfficialHttpApiModule),
    typeof(WeChatManagementOfficialsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class WeChatManagementOfficialsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(WeChatManagementOfficialsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<OfficialsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
