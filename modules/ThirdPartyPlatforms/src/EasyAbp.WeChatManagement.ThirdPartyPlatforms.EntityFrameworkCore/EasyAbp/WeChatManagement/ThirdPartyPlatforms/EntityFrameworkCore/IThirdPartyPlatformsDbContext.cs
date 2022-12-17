using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EntityFrameworkCore;

[ConnectionStringName(ThirdPartyPlatformsDbProperties.ConnectionStringName)]
public interface IThirdPartyPlatformsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<AuthorizerSecret> AuthorizerSecrets { get; set; }
}
