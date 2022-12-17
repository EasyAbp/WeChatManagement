using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

public abstract class ThirdPartyPlatformsAppService : ApplicationService
{
    protected ThirdPartyPlatformsAppService()
    {
        LocalizationResource = typeof(ThirdPartyPlatformsResource);
        ObjectMapperContext = typeof(WeChatManagementThirdPartyPlatformsApplicationModule);
    }
}
