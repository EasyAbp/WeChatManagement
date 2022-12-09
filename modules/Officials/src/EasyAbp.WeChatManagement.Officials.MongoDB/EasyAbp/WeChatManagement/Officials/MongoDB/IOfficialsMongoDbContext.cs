using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.Officials.MongoDB;

[ConnectionStringName(OfficialsDbProperties.ConnectionStringName)]
public interface IOfficialsMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
