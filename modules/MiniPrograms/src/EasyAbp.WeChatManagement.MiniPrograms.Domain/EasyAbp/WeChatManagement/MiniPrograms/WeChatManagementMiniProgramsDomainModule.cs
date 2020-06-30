using Microsoft.Extensions.DependencyInjection;
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
            context.Services.PreConfigure<IIdentityServerBuilder>(builder =>
            {
                builder.AddExtensionGrantValidator<WeChatMiniProgramGrantValidator>();
            });
        }
    }
}
