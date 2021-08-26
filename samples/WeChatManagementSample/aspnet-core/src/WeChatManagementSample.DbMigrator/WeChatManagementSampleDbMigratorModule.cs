using WeChatManagementSample.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace WeChatManagementSample.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(WeChatManagementSampleEntityFrameworkCoreModule),
        typeof(WeChatManagementSampleApplicationContractsModule)
        )]
    public class WeChatManagementSampleDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
