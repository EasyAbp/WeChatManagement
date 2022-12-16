using Volo.Abp.Reflection;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Permissions;

public class ThirdPartyPlatformsPermissions
{
    public const string GroupName = "EasyAbp.WeChatManagement.ThirdPartyPlatforms";

    public class Authorization
    {
        public const string Default = GroupName + ".Authorization";
        public const string CreateRequest = Default + ".CreateRequest";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ThirdPartyPlatformsPermissions));
    }
}