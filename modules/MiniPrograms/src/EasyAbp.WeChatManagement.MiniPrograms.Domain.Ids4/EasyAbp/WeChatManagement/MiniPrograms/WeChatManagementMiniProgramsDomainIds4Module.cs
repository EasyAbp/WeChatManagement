using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.MiniPrograms;

[DependsOn(
    typeof(AbpIdentityServerDomainModule),
    typeof(WeChatManagementMiniProgramsDomainModule)
)]
public class WeChatManagementMiniProgramsDomainIds4Module : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<IIdentityServerBuilder>(builder =>
        {
            builder.AddExtensionGrantValidator<WeChatMiniProgramGrantValidator>();
        });
    }
}