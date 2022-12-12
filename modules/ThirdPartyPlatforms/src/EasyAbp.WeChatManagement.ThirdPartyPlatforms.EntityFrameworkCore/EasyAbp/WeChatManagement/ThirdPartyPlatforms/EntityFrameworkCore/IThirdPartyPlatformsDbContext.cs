using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EntityFrameworkCore;

[ConnectionStringName(ThirdPartyPlatformsDbProperties.ConnectionStringName)]
public interface IThirdPartyPlatformsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
