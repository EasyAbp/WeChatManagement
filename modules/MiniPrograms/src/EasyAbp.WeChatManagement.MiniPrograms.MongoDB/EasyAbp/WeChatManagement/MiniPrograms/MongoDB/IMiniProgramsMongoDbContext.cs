using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.MiniPrograms.MongoDB
{
    [ConnectionStringName(MiniProgramsDbProperties.ConnectionStringName)]
    public interface IMiniProgramsMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
