using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace WeChatManagementSample.Web
{
    [Dependency(ReplaceServices = true)]
    public class WeChatManagementSampleBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "WeChatManagementSample";
    }
}
