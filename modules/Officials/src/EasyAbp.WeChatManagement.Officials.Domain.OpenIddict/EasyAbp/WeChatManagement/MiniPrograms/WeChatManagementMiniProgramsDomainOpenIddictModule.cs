using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Server;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;

namespace EasyAbp.WeChatManagement.Officials;

[DependsOn(
    typeof(AbpOpenIddictAspNetCoreModule),
    typeof(WeChatManagementOfficialsDomainModule)
)]
public class WeChatManagementOfficialsDomainOpenIddictModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictServerBuilder>(builder =>
        {
            builder.Configure(openIddictServerOptions =>
            {
                openIddictServerOptions.GrantTypes.Add(WeChatOfficialConsts.GrantType);
                openIddictServerOptions.Claims.Add(WeChatOfficialConsts.AppIdClaim);
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<AbpOpenIddictExtensionGrantsOptions>(options =>
        {
            options.Grants.Add(WeChatOfficialConsts.GrantType, new WeChatOfficialTokenExtensionGrant());
        });
    }
}