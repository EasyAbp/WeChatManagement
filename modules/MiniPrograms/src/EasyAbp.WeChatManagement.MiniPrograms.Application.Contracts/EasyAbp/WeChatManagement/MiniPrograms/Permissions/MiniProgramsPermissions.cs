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
    }
}