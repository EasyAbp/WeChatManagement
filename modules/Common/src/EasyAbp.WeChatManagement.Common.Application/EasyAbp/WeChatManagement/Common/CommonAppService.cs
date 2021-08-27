using EasyAbp.WeChatManagement.Common.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Common
{
    public abstract class CommonAppService : ApplicationService
    {
        protected CommonAppService()
        {
            LocalizationResource = typeof(CommonResource);
            ObjectMapperContext = typeof(WeChatManagementCommonApplicationModule);
        }
    }
}
