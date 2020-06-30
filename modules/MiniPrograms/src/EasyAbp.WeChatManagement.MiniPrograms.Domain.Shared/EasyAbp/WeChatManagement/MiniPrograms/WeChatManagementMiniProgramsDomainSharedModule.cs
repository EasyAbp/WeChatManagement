using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(AbpValidationModule)
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
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/EasyAbp/WeChatManagement/MiniPrograms/Localization");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.WeChatManagement.MiniPrograms", typeof(MiniProgramsResource));
            });
        }
    }
}
