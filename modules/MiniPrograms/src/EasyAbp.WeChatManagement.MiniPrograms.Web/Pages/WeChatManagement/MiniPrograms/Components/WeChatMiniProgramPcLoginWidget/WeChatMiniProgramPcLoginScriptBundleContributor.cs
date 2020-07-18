using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.Components.WeChatMiniProgramPcLoginWidget
{
    public class WeChatMiniProgramPcLoginScriptBundleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains(
                "/Pages/WeChatManagement/MiniPrograms/Components/WeChatMiniProgramPcLoginWidget/default.js");
        }
    }
}