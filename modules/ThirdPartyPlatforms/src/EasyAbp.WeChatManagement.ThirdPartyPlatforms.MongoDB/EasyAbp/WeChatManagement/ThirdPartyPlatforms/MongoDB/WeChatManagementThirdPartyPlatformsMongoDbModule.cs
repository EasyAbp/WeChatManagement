using EasyAbp.WeChatManagement.Common.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.MongoDB;

[DependsOn(
    typeof(WeChatManagementCommonMongoDbModule),
    typeof(WeChatManagementThirdPartyPlatformsDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class WeChatManagementThirdPartyPlatformsMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<ThirdPartyPlatformsMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
