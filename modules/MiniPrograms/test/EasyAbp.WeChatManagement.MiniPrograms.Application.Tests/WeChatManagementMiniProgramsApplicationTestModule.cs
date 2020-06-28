using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsApplicationModule),
        typeof(WeChatManagementMiniProgramsDomainTestModule)
        )]
    public class WeChatManagementMiniProgramsApplicationTestModule : AbpModule
    {

    }
}
