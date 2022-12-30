using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials.MongoDB
{
    [DependsOn(
        typeof(WeChatManagementOfficialsTestBaseModule),
        typeof(WeChatManagementOfficialsMongoDbModule)
        )]
    public class WeChatManagementOfficialsMongoDbTestModule : AbpModule
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