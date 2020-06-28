using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsDomainSharedModule)
        )]
    public class WeChatManagementMiniProgramsDomainModule : AbpModule
    {

    }
}
