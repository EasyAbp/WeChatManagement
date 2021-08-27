using EasyAbp.WeChatManagement.Common;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Authorization;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule),
        typeof(WeChatManagementCommonApplicationContractsModule)
    )]
    public class WeChatManagementMiniProgramsApplicationContractsModule : AbpModule
    {

    }
}
