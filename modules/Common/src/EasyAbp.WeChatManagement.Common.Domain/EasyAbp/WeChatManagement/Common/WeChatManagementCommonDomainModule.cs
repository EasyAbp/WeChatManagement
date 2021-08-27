using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Common
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(WeChatManagementCommonDomainSharedModule)
    )]
    public class WeChatManagementCommonDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.AutoEventSelectors.Add<WeChatApp>();
            });
        }
    }
}
