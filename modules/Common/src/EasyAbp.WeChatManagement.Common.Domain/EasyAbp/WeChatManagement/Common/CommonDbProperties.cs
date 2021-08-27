namespace EasyAbp.WeChatManagement.Common
{
    public static class CommonDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EasyAbpWeChatManagementCommon";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EasyAbpWeChatManagementCommon";
    }
}
