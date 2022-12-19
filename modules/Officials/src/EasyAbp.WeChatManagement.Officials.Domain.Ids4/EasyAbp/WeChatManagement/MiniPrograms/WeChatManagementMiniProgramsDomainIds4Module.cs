using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials;

[DependsOn(
    typeof(AbpIdentityServerDomainModule),
    typeof(WeChatManagementOfficialsDomainModule)
)]
public class WeChatManagementOfficialsDomainIds4Module : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<IIdentityServerBuilder>(builder =>
        {
            builder.AddExtensionGrantValidator<WeChatOfficialGrantValidator>();
        });
    }
}