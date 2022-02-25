using EasyAbp.WeChatManagement.Officials.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(OfficialsEntityFrameworkCoreTestModule)
    )]
public class OfficialsDomainTestModule : AbpModule
{

}
