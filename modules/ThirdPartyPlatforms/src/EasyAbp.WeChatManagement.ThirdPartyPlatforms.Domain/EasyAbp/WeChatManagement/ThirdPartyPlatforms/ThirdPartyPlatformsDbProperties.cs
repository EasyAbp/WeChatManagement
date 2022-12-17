namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

public static class ThirdPartyPlatformsDbProperties
{
    public static string DbTablePrefix { get; set; } = "EasyAbpWeChatManagementThirdPartyPlatforms";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "EasyAbpWeChatManagementThirdPartyPlatforms";
}
