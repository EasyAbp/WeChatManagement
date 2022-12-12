using EasyAbp.WeChatManagement.ThirdPartyPlatforms.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(ThirdPartyPlatformsEntityFrameworkCoreTestModule)
    )]
public class ThirdPartyPlatformsDomainTestModule : AbpModule
{

}
