using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Server;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;

namespace EasyAbp.WeChatManagement.MiniPrograms;

[DependsOn(
    typeof(AbpOpenIddictAspNetCoreModule),
    typeof(WeChatManagementMiniProgramsDomainModule)
)]
public class WeChatManagementMiniProgramsDomainOpenIddictModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<OpenIddictServerBuilder>(builder =>
        {
            builder.Configure(openIddictServerOptions =>
            {
                openIddictServerOptions.GrantTypes.Add(WeChatMiniProgramConsts.GrantType);
                openIddictServerOptions.Claims.Add(WeChatMiniProgramConsts.AppIdClaim);
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<AbpOpenIddictExtensionGrantsOptions>(options =>
        {
            options.Grants.Add(WeChatMiniProgramConsts.GrantType, new WeChatMiniProgramTokenExtensionGrant());
        });
    }
}