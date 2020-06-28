using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public abstract class MiniProgramsAppService : ApplicationService
    {
        protected MiniProgramsAppService()
        {
            LocalizationResource = typeof(MiniProgramsResource);
            ObjectMapperContext = typeof(WeChatManagementMiniProgramsApplicationModule);
        }
    }
}
