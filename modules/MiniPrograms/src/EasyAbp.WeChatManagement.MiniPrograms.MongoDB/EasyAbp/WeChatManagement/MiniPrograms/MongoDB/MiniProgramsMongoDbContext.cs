using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.MiniPrograms.MongoDB
{
    [ConnectionStringName(MiniProgramsDbProperties.ConnectionStringName)]
    public class MiniProgramsMongoDbContext : AbpMongoDbContext, IMiniProgramsMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureMiniPrograms();
        }
    }
}