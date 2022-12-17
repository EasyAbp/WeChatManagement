using EasyAbp.WeChatManagement.Common.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Localization;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Web;

[DependsOn(
    typeof(WeChatManagementCommonWebModule),
    typeof(WeChatManagementThirdPartyPlatformsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
)]
public class WeChatManagementThirdPartyPlatformsWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(ThirdPartyPlatformsResource),
                typeof(WeChatManagementThirdPartyPlatformsWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(WeChatManagementThirdPartyPlatformsWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ThirdPartyPlatformsMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WeChatManagementThirdPartyPlatformsWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<WeChatManagementThirdPartyPlatformsWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<WeChatManagementThirdPartyPlatformsWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            //Configure authorization.
        });
    }
}