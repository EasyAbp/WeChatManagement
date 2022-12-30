using EasyAbp.WeChatManagement.Common.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using EasyAbp.WeChatManagement.Officials.Localization;
using EasyAbp.WeChatManagement.Officials.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using EasyAbp.WeChatManagement.Officials.Permissions;

namespace EasyAbp.WeChatManagement.Officials.Web
{
    [DependsOn(
        typeof(WeChatManagementOfficialsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule),
        typeof(WeChatManagementCommonWebModule)
    )]
    public class WeChatManagementOfficialsWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(OfficialsResource), typeof(WeChatManagementOfficialsWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(WeChatManagementOfficialsWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new OfficialsMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<WeChatManagementOfficialsWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<WeChatManagementOfficialsWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<WeChatManagementOfficialsWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });
        }
    }
}
