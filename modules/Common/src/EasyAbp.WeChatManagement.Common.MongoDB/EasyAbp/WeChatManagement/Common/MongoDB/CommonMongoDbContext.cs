using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.Common.MongoDB
{
    [ConnectionStringName(CommonDbProperties.ConnectionStringName)]
    public class CommonMongoDbContext : AbpMongoDbContext, ICommonMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureCommon();
        }
    }
}