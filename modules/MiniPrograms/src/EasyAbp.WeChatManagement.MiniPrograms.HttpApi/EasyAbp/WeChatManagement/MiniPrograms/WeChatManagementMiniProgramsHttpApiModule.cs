using EasyAbp.WeChatManagement.Common;
using Localization.Resources.AbpUi;
using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(WeChatManagementCommonHttpApiModule)
    )]
    public class WeChatManagementMiniProgramsHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(WeChatManagementMiniProgramsHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<MiniProgramsResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
