namespace EasyAbp.WeChatManagement.MiniPrograms.Settings
{
    public static class MiniProgramsSettings
    {
        public const string GroupName = "EasyAbp.WeChatManagement.MiniPrograms";

        /* Add constants for setting names. Example:
         * public const string MySettingName = GroupName + ".MySettingName";
         */
        
        public class PcLogin
        {
            private const string PcLoginGroupName = GroupName + ".PcLogin";

            public const string DefaultProgramName = PcLoginGroupName + ".DefaultMiniProgramName";
        
            public const string HandlePage = PcLoginGroupName + ".HandlePage";
        }
    }
}