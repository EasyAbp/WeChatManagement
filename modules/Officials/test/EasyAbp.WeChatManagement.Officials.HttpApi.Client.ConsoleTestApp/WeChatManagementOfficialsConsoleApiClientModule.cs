using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials
{
    [DependsOn(
        typeof(WeChatManagementOfficialsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class WeChatManagementOfficialsConsoleApiClientModule : AbpModule
    {
        
    }
}
