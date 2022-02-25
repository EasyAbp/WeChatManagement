using EasyAbp.Abp.WeChat.Official;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials;

[DependsOn(
    typeof(AbpWeChatOfficialModule),
    typeof(AbpDddDomainModule),
    typeof(WeChatManagementOfficialsDomainSharedModule)
)]
public class WeChatManagementOfficialsDomainModule : AbpModule
{

}
