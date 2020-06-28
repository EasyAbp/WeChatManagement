using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class WeChatManagementMiniProgramsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "MiniPrograms";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(WeChatManagementMiniProgramsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
