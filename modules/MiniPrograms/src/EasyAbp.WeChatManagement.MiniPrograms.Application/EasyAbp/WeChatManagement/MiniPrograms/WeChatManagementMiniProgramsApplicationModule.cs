using System;
using EasyAbp.Abp.WeChat;
using EasyAbp.Abp.WeChat.MiniProgram;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve;
using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Identity.AspNetCore;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsDomainModule),
        typeof(WeChatManagementMiniProgramsApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpWeChatMiniProgramModule),
        typeof(AbpIdentityAspNetCoreModule),
        typeof(WeChatManagementCommonApplicationModule)
    )]
    public class WeChatManagementMiniProgramsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<WeChatManagementMiniProgramsApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<WeChatManagementMiniProgramsApplicationModule>(validate: true);
            });
            
            context.Services.AddHttpClient(WeChatMiniProgramConsts.AuthServerHttpClientName, c =>
            {
                c.Timeout = TimeSpan.FromMilliseconds(5000);
            });
            
            Configure<AbpWeChatMiniProgramResolveOptions>(options =>
            {
                if (!options.Contributors.Exists(x => x.Name == ClaimsWeChatMiniProgramOptionsResolveContributor.ContributorName))
                {
                    options.Contributors.Insert(0, new ClaimsWeChatMiniProgramOptionsResolveContributor());
                }
            });
        }
    }
}
