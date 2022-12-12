using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ThirdPartyPlatformsPageModel : AbpPageModel
{
    protected ThirdPartyPlatformsPageModel()
    {
        LocalizationResourceType = typeof(ThirdPartyPlatformsResource);
        ObjectMapperContext = typeof(WeChatManagementThirdPartyPlatformsWebModule);
    }
}
