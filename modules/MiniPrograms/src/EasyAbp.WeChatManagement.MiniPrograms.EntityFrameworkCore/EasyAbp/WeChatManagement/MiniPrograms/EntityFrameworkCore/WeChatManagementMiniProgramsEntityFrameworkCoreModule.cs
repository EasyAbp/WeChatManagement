using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class WeChatManagementMiniProgramsEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MiniProgramsDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            });
        }
    }
}