using EasyAbp.WeChatManagement.Officials.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.WeChatManagement.Officials.Pages;

public abstract class OfficialsPageModel : AbpPageModel
{
    protected OfficialsPageModel()
    {
        LocalizationResourceType = typeof(OfficialsResource);
    }
}
