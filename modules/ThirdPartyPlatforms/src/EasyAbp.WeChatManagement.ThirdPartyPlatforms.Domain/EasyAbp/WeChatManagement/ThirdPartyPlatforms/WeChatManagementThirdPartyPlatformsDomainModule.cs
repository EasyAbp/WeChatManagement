using EasyAbp.Abp.WeChat.OpenPlatform;
using EasyAbp.WeChatManagement.Common;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[DependsOn(
    typeof(AbpWeChatOpenPlatformModule),
    typeof(WeChatManagementCommonDomainModule),
    typeof(AbpDddDomainModule),
    typeof(WeChatManagementThirdPartyPlatformsDomainSharedModule)
)]
public class WeChatManagementThirdPartyPlatformsDomainModule : AbpModule
{
}