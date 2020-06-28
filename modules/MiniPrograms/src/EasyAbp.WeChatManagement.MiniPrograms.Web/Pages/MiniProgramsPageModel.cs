using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class MiniProgramsPageModel : AbpPageModel
    {
        protected MiniProgramsPageModel()
        {
            LocalizationResourceType = typeof(MiniProgramsResource);
            ObjectMapperContext = typeof(WeChatManagementMiniProgramsWebModule);
        }
    }
}