using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
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
                options.AddRepository<MiniProgram, MiniProgramRepository>();
                options.AddRepository<MiniProgramUser, MiniProgramUserRepository>();
                options.AddRepository<UserInfo, UserInfoRepository>();
            });
        }
    }
}
