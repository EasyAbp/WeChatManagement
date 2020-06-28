using Volo.Abp.Modularity;

namespace WeChatManagementSample
{
    [DependsOn(
        typeof(WeChatManagementSampleApplicationModule),
        typeof(WeChatManagementSampleDomainTestModule)
        )]
    public class WeChatManagementSampleApplicationTestModule : AbpModule
    {

    }
}