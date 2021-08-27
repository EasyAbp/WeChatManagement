using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.Common.MongoDB
{
    [DependsOn(
        typeof(WeChatManagementCommonDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class WeChatManagementCommonMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<CommonMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
