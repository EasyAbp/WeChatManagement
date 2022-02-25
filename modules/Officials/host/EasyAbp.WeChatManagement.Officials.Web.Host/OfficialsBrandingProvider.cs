using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.Officials;

[Dependency(ReplaceServices = true)]
public class OfficialsBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Officials";
}
