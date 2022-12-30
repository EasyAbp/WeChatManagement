using System;
using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Identity.AspNetCore;

namespace EasyAbp.WeChatManagement.Officials
{
    [DependsOn(
        typeof(WeChatManagementOfficialsDomainModule),
        typeof(WeChatManagementOfficialsApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpIdentityAspNetCoreModule),
        typeof(WeChatManagementCommonApplicationModule)
    )]
    public class WeChatManagementOfficialsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<WeChatManagementOfficialsApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<WeChatManagementOfficialsApplicationModule>(validate: true);
            });

            context.Services.AddHttpClient(WeChatOfficialConsts.AuthServerHttpClientName,
                c => { c.Timeout = TimeSpan.FromMilliseconds(5000); });
        }
    }
}