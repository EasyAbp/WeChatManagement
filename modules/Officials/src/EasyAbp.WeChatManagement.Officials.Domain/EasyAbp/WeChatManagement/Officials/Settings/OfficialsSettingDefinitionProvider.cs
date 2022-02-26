using EasyAbp.WeChatManagement.Officials.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EasyAbp.WeChatManagement.Officials.Settings;

public class OfficialsSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        /* Define module settings here.
         * Use names from OfficialsSettings class.
         */

        context.Add(new SettingDefinition(
            OfficialsSettings.Login.DefaultProgramName,
            "Default",
            L("Setting:DefaultLoginWeChatAppName"),
            isVisibleToClients: true
        ));

        context.Add(new SettingDefinition(
            OfficialsSettings.Login.HandlePage,
            null,
            L("Setting:LoginHandlePage"),
            isVisibleToClients: true
        ));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<OfficialsResource>(name);
    }
}
