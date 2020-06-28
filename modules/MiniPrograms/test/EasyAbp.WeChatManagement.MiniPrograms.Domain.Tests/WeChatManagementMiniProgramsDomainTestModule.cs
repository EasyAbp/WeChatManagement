using EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(WeChatManagementMiniProgramsEntityFrameworkCoreTestModule)
        )]
    public class WeChatManagementMiniProgramsDomainTestModule : AbpModule
    {
        
    }
}
