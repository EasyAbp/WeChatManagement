using System;
using System.IO;
using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.Common.Web;
using EasyAbp.WeChatManagement.MiniPrograms;
using EasyAbp.WeChatManagement.MiniPrograms.Web;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Web;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeChatManagementSample.EntityFrameworkCore;
using WeChatManagementSample.Localization;
using WeChatManagementSample.MultiTenancy;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using WeChatManagementSample.Web.Ids4.Menus;

namespace WeChatManagementSample.Web.Ids4
{
    [DependsOn(
        typeof(WeChatManagementSampleHttpApiModule),
        typeof(WeChatManagementSampleApplicationModule),
        typeof(WeChatManagementSampleEntityFrameworkCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpTenantManagementWebModule),
        typeof(AbpFeatureManagementWebModule),
        typeof(AbpSettingManagementWebModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(WeChatManagementMiniProgramsWebModule),
        typeof(WeChatManagementMiniProgramsDomainIds4Module),
        typeof(WeChatManagementThirdPartyPlatformsWebModule)
    )]
    public class WeChatManagementSampleWebIds4Module : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(WeChatManagementSampleResource),
                    typeof(WeChatManagementSampleDomainModule).Assembly,
                    typeof(WeChatManagementSampleDomainSharedModule).Assembly,
                    typeof(WeChatManagementSampleApplicationModule).Assembly,
                    typeof(WeChatManagementSampleApplicationContractsModule).Assembly,
                    typeof(WeChatManagementSampleWebIds4Module).Assembly
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureUrls(configuration);
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = "WeChatManagementSample";
                });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<WeChatManagementSampleWebIds4Module>();

            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementSampleDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}WeChatManagementSample.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementSampleDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}WeChatManagementSample.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementSampleApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}WeChatManagementSample.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementSampleApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}WeChatManagementSample.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementSampleWebIds4Module>(hostingEnvironment.ContentRootPath);
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementCommonDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}Common{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.WeChatManagement.Common.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementCommonDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}Common{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.WeChatManagement.Common.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementCommonApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}Common{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.WeChatManagement.Common.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementCommonApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}Common{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.WeChatManagement.Common.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementCommonWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}Common{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.WeChatManagement.Common.Web"));
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementMiniProgramsDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}MiniPrograms{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementMiniProgramsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}MiniPrograms{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.WeChatManagement.MiniPrograms.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementMiniProgramsApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}MiniPrograms{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.WeChatManagement.MiniPrograms.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementMiniProgramsApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}MiniPrograms{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.WeChatManagement.MiniPrograms.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<WeChatManagementMiniProgramsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}MiniPrograms{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.WeChatManagement.MiniPrograms.Web"));
                });
            }
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<WeChatManagementSampleResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );

                options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureNavigationServices()
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new WeChatManagementSampleMenuContributor());
            });
        }

        private void ConfigureAutoApiControllers()
        {
            PreConfigure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(WeChatManagementSampleApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WeChatManagementSample API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseRouting();
            app.MapAbpStaticAssets();
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseUnitOfWork();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WeChatManagementSample API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
