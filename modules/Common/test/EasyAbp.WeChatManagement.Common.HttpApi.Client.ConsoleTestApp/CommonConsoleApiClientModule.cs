using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Common
{
    [DependsOn(
        typeof(WeChatManagementCommonHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class CommonConsoleApiClientModule : AbpModule
    {
        
    }
}
