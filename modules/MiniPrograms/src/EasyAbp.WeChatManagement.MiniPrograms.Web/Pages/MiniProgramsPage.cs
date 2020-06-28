using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.MiniProgramsPage
     */
    public abstract class MiniProgramsPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<MiniProgramsResource> L { get; set; }
    }
}
