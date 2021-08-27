using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Common
{
    [DependsOn(
        typeof(WeChatManagementCommonApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class WeChatManagementCommonHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EasyAbpWeChatManagementCommon";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(WeChatManagementCommonApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
