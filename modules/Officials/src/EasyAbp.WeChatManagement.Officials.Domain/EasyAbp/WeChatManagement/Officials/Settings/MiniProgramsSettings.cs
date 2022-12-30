namespace EasyAbp.WeChatManagement.Officials.Settings
{
    public static class OfficialsSettings
    {
        public const string GroupName = "EasyAbp.WeChatManagement.Officials";

        /* Add constants for setting names. Example:
         * public const string MySettingName = GroupName + ".MySettingName";
         */
        
        public class PcLogin
        {
            private const string PcLoginGroupName = GroupName + ".PcLogin";

            public const string DefaultProgramName = PcLoginGroupName + ".DefaultOfficialName";
        
            public const string HandlePage = PcLoginGroupName + ".HandlePage";
        }
    }
}