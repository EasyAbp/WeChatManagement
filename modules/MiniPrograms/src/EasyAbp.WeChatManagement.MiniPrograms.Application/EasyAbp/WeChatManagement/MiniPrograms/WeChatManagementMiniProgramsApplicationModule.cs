using System;
using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Identity.AspNetCore;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsDomainModule),
        typeof(WeChatManagementMiniProgramsApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpMapperlyModule),
        typeof(AbpIdentityAspNetCoreModule),
        typeof(WeChatManagementCommonApplicationModule)
    )]
    public class WeChatManagementMiniProgramsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapperlyObjectMapper<WeChatManagementMiniProgramsApplicationModule>();

            context.Services.AddHttpClient(WeChatMiniProgramConsts.AuthServerHttpClientName,
                c => { c.Timeout = TimeSpan.FromMilliseconds(5000); });
        }
    }
}