using EasyAbp.WeChatManagement.MiniPrograms;
using EasyAbp.WeChatManagement.Officials;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace WeChatManagementSample
{
    [DependsOn(
        typeof(WeChatManagementSampleApplicationContractsModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(AbpSettingManagementHttpApiModule),
        typeof(WeChatManagementMiniProgramsHttpApiModule),
        typeof(WeChatManagementOfficialsHttpApiModule)
    )]
    public class WeChatManagementSampleHttpApiModule : AbpModule
    {
        
    }
}
