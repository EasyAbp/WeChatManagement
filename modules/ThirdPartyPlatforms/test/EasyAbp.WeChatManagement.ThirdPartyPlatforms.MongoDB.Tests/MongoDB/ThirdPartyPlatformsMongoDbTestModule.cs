using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.MongoDB;

[DependsOn(
    typeof(ThirdPartyPlatformsTestBaseModule),
    typeof(WeChatManagementThirdPartyPlatformsMongoDbModule)
    )]
public class ThirdPartyPlatformsMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
