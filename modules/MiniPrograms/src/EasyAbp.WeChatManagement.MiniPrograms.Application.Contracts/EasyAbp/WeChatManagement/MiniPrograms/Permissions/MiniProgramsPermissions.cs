using Volo.Abp.Reflection;

namespace EasyAbp.WeChatManagement.MiniPrograms.Permissions
{
    public class MiniProgramsPermissions
    {
        public const string GroupName = "EasyAbp.WeChatManagement.MiniPrograms";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(MiniProgramsPermissions));
        }

        public class MiniProgram
        {
            public const string Default = GroupName + ".MiniProgram";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class MiniProgramUser
        {
            public const string Default = GroupName + ".MiniProgramUser";
            public const string Manage = Default + ".Manage";
        }

        public class UserInfo
        {
            public const string Default = GroupName + ".UserInfo";
        }
    }
}
