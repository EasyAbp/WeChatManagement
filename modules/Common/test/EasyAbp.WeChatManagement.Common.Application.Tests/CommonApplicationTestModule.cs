using Volo.Abp.Modularity;

namespace EasyAbp.WeChatManagement.Common
{
    [DependsOn(
        typeof(WeChatManagementCommonApplicationModule),
        typeof(CommonDomainTestModule)
        )]
    public class CommonApplicationTestModule : AbpModule
    {

    }
}
