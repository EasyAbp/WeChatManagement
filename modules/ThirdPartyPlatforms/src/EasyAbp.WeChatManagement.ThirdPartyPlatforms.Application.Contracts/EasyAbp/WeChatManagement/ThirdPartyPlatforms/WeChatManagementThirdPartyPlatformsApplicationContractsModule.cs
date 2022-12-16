using EasyAbp.Abp.WeChat.OpenPlatform;
using EasyAbp.WeChatManagement.Common;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(AbpWeChatOpenPlatformAbstractionsModule),
    typeof(WeChatManagementCommonApplicationContractsModule),
    typeof(WeChatManagementThirdPartyPlatformsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class WeChatManagementThirdPartyPlatformsApplicationContractsModule : AbpModule
{

}
