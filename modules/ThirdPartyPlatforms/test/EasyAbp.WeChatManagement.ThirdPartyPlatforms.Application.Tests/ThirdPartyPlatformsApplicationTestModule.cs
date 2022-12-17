using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(WeChatManagementThirdPartyPlatformsApplicationModule),
    typeof(ThirdPartyPlatformsDomainTestModule)
    )]
public class ThirdPartyPlatformsApplicationTestModule : AbpModule
{

}
