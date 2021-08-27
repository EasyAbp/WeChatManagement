using EasyAbp.WeChatManagement.Common.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.WeChatManagement.Common.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class CommonPageModel : AbpPageModel
    {
        protected CommonPageModel()
        {
            LocalizationResourceType = typeof(CommonResource);
            ObjectMapperContext = typeof(WeChatManagementCommonWebModule);
        }
    }
}