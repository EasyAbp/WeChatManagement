using EasyAbp.WeChatManagement.Common;
using Volo.Abp.Modularity;
using Volo.Abp.Users;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(AbpUsersAbstractionModule),
        typeof(WeChatManagementMiniProgramsDomainSharedModule),
        typeof(WeChatManagementCommonDomainModule)
    )]
    public class WeChatManagementMiniProgramsDomainModule : AbpModule
    {
    }
}
