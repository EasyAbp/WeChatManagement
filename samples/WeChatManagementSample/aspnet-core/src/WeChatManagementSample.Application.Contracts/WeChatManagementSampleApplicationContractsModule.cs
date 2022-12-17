using EasyAbp.WeChatManagement.MiniPrograms;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace WeChatManagementSample
{
    [DependsOn(
        typeof(WeChatManagementSampleDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule),
        typeof(WeChatManagementMiniProgramsApplicationContractsModule),
        typeof(WeChatManagementThirdPartyPlatformsApplicationContractsModule)
    )]
    public class WeChatManagementSampleApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            WeChatManagementSampleDtoExtensions.Configure();
        }
    }
}
