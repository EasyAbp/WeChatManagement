using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.WeChatManagement.Common
{
    [DependsOn(
        typeof(WeChatManagementCommonDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class WeChatManagementCommonApplicationContractsModule : AbpModule
    {

    }
}
