using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.Officials.MongoDB;

[DependsOn(
    typeof(WeChatManagementOfficialsDomainModule),
    typeof(AbpMongoDbModule),
    typeof(WeChatManagementCommonDomainModule)
)]
public class WeChatManagementOfficialsMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<OfficialsMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
