using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.MiniPrograms.MongoDB
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsTestBaseModule),
        typeof(WeChatManagementMiniProgramsMongoDbModule)
        )]
    public class WeChatManagementMiniProgramsMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = MongoDbFixture.ConnectionString.EnsureEndsWith('/') +
                                   "Db_" +
                                    Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}