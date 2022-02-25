using EasyAbp.WeChatManagement.Officials.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.WeChatManagement.Officials.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class OfficialsPageModel : AbpPageModel
{
    protected OfficialsPageModel()
    {
        LocalizationResourceType = typeof(OfficialsResource);
        ObjectMapperContext = typeof(WeChatManagementOfficialsWebModule);
    }
}
