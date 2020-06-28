using WeChatManagementSample.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace WeChatManagementSample.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class WeChatManagementSamplePageModel : AbpPageModel
    {
        protected WeChatManagementSamplePageModel()
        {
            LocalizationResourceType = typeof(WeChatManagementSampleResource);
        }
    }
}