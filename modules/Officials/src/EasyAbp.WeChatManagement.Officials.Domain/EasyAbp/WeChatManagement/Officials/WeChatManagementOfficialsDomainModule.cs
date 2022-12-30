using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Official;
using EasyAbp.Abp.WeChat.Official.Options;
using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Users;

namespace EasyAbp.WeChatManagement.Officials
{
    [DependsOn(
        typeof(AbpWeChatOfficialModule),
        typeof(AbpUsersAbstractionModule),
        typeof(WeChatManagementOfficialsDomainSharedModule),
        typeof(WeChatManagementCommonDomainModule)
    )]
    public class WeChatManagementOfficialsDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services
                .AddTransient<IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions>,
                    WeChatManagementOfficialAbpWeChatOptionsProvider>();
        }
    }
}