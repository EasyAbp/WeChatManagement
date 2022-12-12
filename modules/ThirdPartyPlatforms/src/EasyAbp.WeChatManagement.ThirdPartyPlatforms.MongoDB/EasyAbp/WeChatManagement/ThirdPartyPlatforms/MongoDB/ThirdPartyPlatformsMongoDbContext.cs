using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.MongoDB;

[ConnectionStringName(ThirdPartyPlatformsDbProperties.ConnectionStringName)]
public class ThirdPartyPlatformsMongoDbContext : AbpMongoDbContext, IThirdPartyPlatformsMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureThirdPartyPlatforms();
    }
}
