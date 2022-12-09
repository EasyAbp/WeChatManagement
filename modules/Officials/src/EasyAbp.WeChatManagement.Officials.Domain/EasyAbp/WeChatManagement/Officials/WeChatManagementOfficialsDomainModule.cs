using EasyAbp.Abp.WeChat.Official;
using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials;

[DependsOn(
    typeof(AbpWeChatOfficialModule),
    typeof(AbpIdentityServerDomainModule),
    typeof(WeChatManagementOfficialsDomainSharedModule),
    typeof(WeChatManagementCommonDomainModule)
)]
public class WeChatManagementOfficialsDomainModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<IIdentityServerBuilder>(builder =>
        {
            builder.AddExtensionGrantValidator<WeChatOfficialGrantValidator>();
        });
    }
}
