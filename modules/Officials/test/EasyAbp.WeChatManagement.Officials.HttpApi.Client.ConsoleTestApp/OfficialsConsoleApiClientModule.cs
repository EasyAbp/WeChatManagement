using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(WeChatManagementOfficialsHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class OfficialsConsoleApiClientModule : AbpModule
{

}
