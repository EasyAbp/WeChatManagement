using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace WeChatManagementSample.EntityFrameworkCore
{
    [DependsOn(
        typeof(WeChatManagementSampleEntityFrameworkCoreModule)
        )]
    public class WeChatManagementSampleEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<WeChatManagementSampleMigrationsDbContext>();
        }
    }
}
