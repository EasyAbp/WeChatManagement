using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.WeChatManagement.Officials;

[DependsOn(
    typeof(WeChatManagementOfficialsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class WeChatManagementOfficialsApplicationContractsModule : AbpModule
{

}
