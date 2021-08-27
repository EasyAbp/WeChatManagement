using Volo.Abp.Reflection;

namespace EasyAbp.WeChatManagement.Common.Permissions
{
    public class CommonPermissions
    {
        public const string GroupName = "EasyAbp.WeChatManagement.Common";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CommonPermissions));
        }
        
        public class WeChatApp
        {
            public const string Default = GroupName + ".WeChatApp";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class WeChatAppUser
        {
            public const string Default = GroupName + ".WeChatAppUser";
        }
    }
}