using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace EasyAbp.WeChatManagement.Officials.Web.Pages.WeChatManagement.Officials.Components.WeChatOfficialLoginWidget
{
    public class WeChatOfficialLoginStyleBundleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains(
                "/Pages/WeChatManagement/Officials/Components/WeChatOfficialLoginWidget/default.css");
        }
    }
}
