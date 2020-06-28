using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [DependsOn(
        typeof(WeChatManagementMiniProgramsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class WeChatManagementMiniProgramsConsoleApiClientModule : AbpModule
    {
        
    }
}
