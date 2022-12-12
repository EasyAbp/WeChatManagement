using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(WeChatManagementThirdPartyPlatformsHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class ThirdPartyPlatformsConsoleApiClientModule : AbpModule
{

}
