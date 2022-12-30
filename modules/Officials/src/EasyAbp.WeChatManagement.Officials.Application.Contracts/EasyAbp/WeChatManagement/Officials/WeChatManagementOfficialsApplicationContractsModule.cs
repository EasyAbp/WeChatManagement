using EasyAbp.WeChatManagement.Common;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Authorization;

namespace EasyAbp.WeChatManagement.Officials
{
    [DependsOn(
        typeof(WeChatManagementOfficialsDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule),
        typeof(WeChatManagementCommonApplicationContractsModule)
    )]
    public class WeChatManagementOfficialsApplicationContractsModule : AbpModule
    {

    }
}
