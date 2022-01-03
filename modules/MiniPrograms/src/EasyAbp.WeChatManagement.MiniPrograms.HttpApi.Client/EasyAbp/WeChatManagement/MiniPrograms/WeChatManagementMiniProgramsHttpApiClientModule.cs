using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsApplicationContractsModule),
        typeof(AbpHttpClientModule),
        typeof(WeChatManagementCommonHttpApiClientModule)
    )]
    public class WeChatManagementMiniProgramsHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = WeChatManagementRemoteServiceConsts.RemoteServiceName;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(WeChatManagementMiniProgramsApplicationContractsModule).Assembly,
                RemoteServiceName
            );
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<WeChatManagementMiniProgramsHttpApiClientModule>();
            });
        }
    }
}
