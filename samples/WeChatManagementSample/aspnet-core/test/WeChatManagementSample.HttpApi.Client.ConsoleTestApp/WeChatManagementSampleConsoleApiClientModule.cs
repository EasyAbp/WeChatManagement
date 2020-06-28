using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace WeChatManagementSample.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(WeChatManagementSampleHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class WeChatManagementSampleConsoleApiClientModule : AbpModule
    {
        
    }
}
