using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.MiniProgram;
using EasyAbp.Abp.WeChat.MiniProgram.Options;
using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Users;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(AbpWeChatMiniProgramModule),
        typeof(AbpUsersAbstractionModule),
        typeof(WeChatManagementMiniProgramsDomainSharedModule),
        typeof(WeChatManagementCommonDomainModule)
    )]
    public class WeChatManagementMiniProgramsDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services
                .AddTransient<IAbpWeChatOptionsProvider<AbpWeChatMiniProgramOptions>,
                    WeChatManagementMiniProgramAbpWeChatOptionsProvider>();
        }
    }
}