using EasyAbp.WeChatManagement.Common;
using Localization.Resources.AbpUi;
using EasyAbp.WeChatManagement.Officials.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.WeChatManagement.Officials
{
    [DependsOn(
        typeof(WeChatManagementOfficialsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(WeChatManagementCommonHttpApiModule)
    )]
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
}
