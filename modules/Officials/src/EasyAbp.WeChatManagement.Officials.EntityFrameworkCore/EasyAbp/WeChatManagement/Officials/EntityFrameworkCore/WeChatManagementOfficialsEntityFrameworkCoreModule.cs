using EasyAbp.WeChatManagement.Common.EntityFrameworkCore;
using EasyAbp.WeChatManagement.Officials.UserInfos;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials.EntityFrameworkCore;

[DependsOn(
    typeof(WeChatManagementOfficialsDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(WeChatManagementCommonEntityFrameworkCoreModule)
)]
public class WeChatManagementOfficialsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<OfficialsDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<UserInfo, UserInfoRepository>();
        });
    }
}
