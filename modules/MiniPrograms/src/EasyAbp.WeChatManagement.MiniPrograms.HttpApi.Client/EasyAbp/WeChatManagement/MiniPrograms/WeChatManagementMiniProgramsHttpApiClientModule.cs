using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsApplicationContractsModule),
        typeof(AbpHttpClientModule),
        typeof(WeChatManagementCommonHttpApiClientModule)
    )]
    public class WeChatManagementMiniProgramsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EasyAbpWeChatManagementMiniPrograms";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(WeChatManagementMiniProgramsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
