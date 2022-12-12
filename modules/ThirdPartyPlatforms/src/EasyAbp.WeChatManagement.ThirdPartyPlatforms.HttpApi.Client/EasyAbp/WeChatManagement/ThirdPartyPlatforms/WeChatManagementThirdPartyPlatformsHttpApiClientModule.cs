using EasyAbp.WeChatManagement.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(WeChatManagementCommonHttpApiClientModule),
    typeof(WeChatManagementThirdPartyPlatformsApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class WeChatManagementThirdPartyPlatformsHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(WeChatManagementThirdPartyPlatformsApplicationContractsModule).Assembly,
            WeChatManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WeChatManagementThirdPartyPlatformsHttpApiClientModule>();
        });

    }
}
