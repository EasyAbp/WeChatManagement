using Volo.Abp.Settings;

namespace WeChatManagementSample.Settings
{
    public class WeChatManagementSampleSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(WeChatManagementSampleSettings.MySetting1));
        }
    }
}
