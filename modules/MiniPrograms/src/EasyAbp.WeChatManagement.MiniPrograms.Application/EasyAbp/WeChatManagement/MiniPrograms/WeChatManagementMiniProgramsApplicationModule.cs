using System;
using EasyAbp.Abp.WeChat;
using EasyAbp.Abp.WeChat.MiniProgram;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsDomainModule),
        typeof(WeChatManagementMiniProgramsApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpWeChatMiniProgramModule)
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
            
            context.Services.AddHttpClient(WeChatMiniProgramConsts.IdentityServerHttpClientName, c =>
            {
                c.Timeout = TimeSpan.FromMilliseconds(5000);
            });
            
            Configure<AbpWeChatMiniProgramResolveOptions>(options =>
            {
                options.Contributors.InsertRange(0, new IWeChatMiniProgramOptionsResolveContributor[]
                {
                    new AsyncLocalWeChatMiniProgramOptionsResolveContributor(),
                    new ClaimsWeChatMiniProgramOptionsResolveContributor()
                });
            });
        }
    }
}
