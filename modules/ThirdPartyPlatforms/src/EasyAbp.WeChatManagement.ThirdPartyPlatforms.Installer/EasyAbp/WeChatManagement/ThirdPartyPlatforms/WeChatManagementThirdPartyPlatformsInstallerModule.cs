using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class WeChatManagementThirdPartyPlatformsInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WeChatManagementThirdPartyPlatformsInstallerModule>();
        });
    }
}
