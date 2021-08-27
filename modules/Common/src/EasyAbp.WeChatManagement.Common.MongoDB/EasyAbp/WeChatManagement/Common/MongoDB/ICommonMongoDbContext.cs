using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.Common.MongoDB
{
    [ConnectionStringName(CommonDbProperties.ConnectionStringName)]
    public interface ICommonMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
