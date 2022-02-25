using EasyAbp.WeChatManagement.Officials.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Officials;

public abstract class OfficialsAppService : ApplicationService
{
    protected OfficialsAppService()
    {
        LocalizationResource = typeof(OfficialsResource);
        ObjectMapperContext = typeof(WeChatManagementOfficialsApplicationModule);
    }
}
