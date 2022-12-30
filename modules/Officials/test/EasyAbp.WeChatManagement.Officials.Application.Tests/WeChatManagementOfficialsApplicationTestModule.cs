using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials
{
    [DependsOn(
        typeof(WeChatManagementOfficialsApplicationModule),
        typeof(WeChatManagementOfficialsDomainTestModule)
        )]
    public class WeChatManagementOfficialsApplicationTestModule : AbpModule
    {

    }
}
