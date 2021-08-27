using Localization.Resources.AbpUi;
using EasyAbp.WeChatManagement.Common.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.WeChatManagement.Common
{
    [DependsOn(
        typeof(WeChatManagementCommonApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class WeChatManagementCommonHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(WeChatManagementCommonHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<CommonResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
