using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace WeChatManagementSample.Web
{
    [Dependency(ReplaceServices = true)]
    public class WeChatManagementSampleBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "WeChatManagementSample";
    }
}
