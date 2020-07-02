using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(AbpIdentityServerDomainModule),
        typeof(WeChatManagementMiniProgramsDomainSharedModule)
        )]
    public class WeChatManagementMiniProgramsDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.AutoEventSelectors.Add<MiniProgram>();
            });
            
            context.Services.PreConfigure<IIdentityServerBuilder>(builder =>
            {
                builder.AddExtensionGrantValidator<WeChatMiniProgramGrantValidator>();
            });
        }
    }
}
