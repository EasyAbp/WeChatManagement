using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Common.EntityFrameworkCore
{
    [DependsOn(
        typeof(WeChatManagementCommonDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class WeChatManagementCommonEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CommonDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<WeChatApp, WeChatAppRepository>();
                options.AddRepository<WeChatAppUser, WeChatAppUserRepository>();
            });
        }
    }
}