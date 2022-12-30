using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.Common.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.WeChatManagement.Officials.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.WeChatManagement.Officials
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(WeChatManagementCommonDomainSharedModule)
    )]
    public class WeChatManagementOfficialsDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<WeChatManagementOfficialsDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<OfficialsResource>("en")
                    .AddBaseTypes(typeof(CommonResource))
                    .AddVirtualJson("/EasyAbp/WeChatManagement/Officials/Localization");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.WeChatManagement.Officials", typeof(OfficialsResource));
            });
        }
    }
}
