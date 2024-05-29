using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.Components.WeChatMiniProgramPcBindWidget
{
    public class WeChatMiniProgramPcBindStyleBundleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains(
                "/Pages/WeChatManagement/MiniPrograms/Components/WeChatMiniProgramPcBindWidget/default.css");
        }
    }
}
