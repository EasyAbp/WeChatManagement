using Volo.Abp.Reflection;

namespace EasyAbp.WeChatManagement.Officials.Permissions;

public class OfficialsPermissions
{
    public const string GroupName = "EasyAbp.WeChatManagement.Officials";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(OfficialsPermissions));
    }

    public class UserInfo
    {
        public const string Default = GroupName + ".UserInfo";
    }
}
