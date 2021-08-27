using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EasyAbp.WeChatManagement.MiniPrograms.Settings
{
    public class MiniProgramsSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from MiniProgramsSettings class.
             */
            
            context.Add(new SettingDefinition(
                MiniProgramsSettings.PcLogin.DefaultProgramName, 
                "Default",
                L("Setting:DefaultPcLoginWeChatAppName"),
                isVisibleToClients: true
            ));
            
            context.Add(new SettingDefinition(
                MiniProgramsSettings.PcLogin.HandlePage, 
                null,
                L("Setting:PcLoginHandlePage"),
                isVisibleToClients: true
            ));
        }
        
        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MiniProgramsResource>(name);
        }
    }
}