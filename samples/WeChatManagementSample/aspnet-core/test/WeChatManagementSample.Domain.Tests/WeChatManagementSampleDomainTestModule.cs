using WeChatManagementSample.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace WeChatManagementSample
{
    [DependsOn(
        typeof(WeChatManagementSampleEntityFrameworkCoreTestModule)
        )]
    public class WeChatManagementSampleDomainTestModule : AbpModule
    {

    }
}