using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using EasyAbp.WeChatManagement.Common.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EntityFrameworkCore;

[DependsOn(
    typeof(WeChatManagementCommonEntityFrameworkCoreModule),
    typeof(WeChatManagementThirdPartyPlatformsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class WeChatManagementThirdPartyPlatformsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ThirdPartyPlatformsDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            options.AddRepository<AuthorizerSecret, AuthorizerSecretRepository>();
        });
    }
}
