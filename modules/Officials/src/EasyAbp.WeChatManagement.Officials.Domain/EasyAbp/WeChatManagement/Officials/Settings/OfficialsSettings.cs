namespace EasyAbp.WeChatManagement.Officials.Settings;

public static class OfficialsSettings
{
    public const string GroupName = "EasyAbp.WeChatManagement.Officials";

    /* Add constants for setting names. Example:
     * public const string MySettingName = GroupName + ".MySettingName";
     */

    public class Login
    {
        private const string LoginGroupName = GroupName + ".Login";

        public const string DefaultProgramName = LoginGroupName + ".DefaultOfficialName";

        public const string HandlePage = LoginGroupName + ".HandlePage";
    }
}
