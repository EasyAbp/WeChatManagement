using WeChatManagementSample.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace WeChatManagementSample.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpOpenIddictEntityFrameworkCoreModule),
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
