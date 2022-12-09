namespace EasyAbp.WeChatManagement.Officials;

public static class OfficialsDbProperties
{
    public static string DbTablePrefix { get; set; } = "EasyAbpWeChatManagementOfficials";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "EasyAbpWeChatManagementOfficials";
}
