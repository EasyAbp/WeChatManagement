using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.MiniPrograms.MongoDB
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsDomainModule),
        typeof(AbpMongoDbModule),
        typeof(WeChatManagementCommonDomainModule)
    )]
    public class WeChatManagementMiniProgramsMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<MiniProgramsMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
