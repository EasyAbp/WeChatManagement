using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.Common.Localization;
using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Volo.Abp.Account;
using Volo.Abp.Domain;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpValidationModule),
        typeof(AbpDddDomainSharedModule),
        typeof(WeChatManagementCommonDomainSharedModule)
    )]
    public class WeChatManagementMiniProgramsDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<WeChatManagementMiniProgramsDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<MiniProgramsResource>("en")
                    .AddBaseTypes(typeof(CommonResource))
                    .AddVirtualJson("/EasyAbp/WeChatManagement/MiniPrograms/Localization");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.WeChatManagement.MiniPrograms", typeof(MiniProgramsResource));
            });
        }
    }
}
