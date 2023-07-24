using EasyAbp.WeChatManagement.Common;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(WeChatManagementCommonDomainSharedModule),
    typeof(AbpValidationModule),
    typeof(AbpDddDomainSharedModule)
)]
public class WeChatManagementThirdPartyPlatformsDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WeChatManagementThirdPartyPlatformsDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<ThirdPartyPlatformsResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/EasyAbp/WeChatManagement/ThirdPartyPlatforms/Localization");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("EasyAbp.WeChatManagement.ThirdPartyPlatforms",
                typeof(ThirdPartyPlatformsResource));
        });
    }
}