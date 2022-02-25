using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Officials;

[DependsOn(
    typeof(WeChatManagementOfficialsApplicationModule),
    typeof(OfficialsDomainTestModule)
    )]
public class OfficialsApplicationTestModule : AbpModule
{

}
