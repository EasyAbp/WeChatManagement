using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.WeChatManagement.MiniPrograms.Permissions
{
    public class MiniProgramsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(MiniProgramsPermissions.GroupName, L("Permission:MiniPrograms"));

            var miniProgramPermission = myGroup.AddPermission(MiniProgramsPermissions.MiniProgram.Default, L("Permission:MiniProgram"));
            miniProgramPermission.AddChild(MiniProgramsPermissions.MiniProgram.Create, L("Permission:Create"));
            miniProgramPermission.AddChild(MiniProgramsPermissions.MiniProgram.Update, L("Permission:Update"));
            miniProgramPermission.AddChild(MiniProgramsPermissions.MiniProgram.Delete, L("Permission:Delete"));

            var miniProgramUserPermission = myGroup.AddPermission(MiniProgramsPermissions.MiniProgramUser.Default, L("Permission:MiniProgramUser"));

            var userInfoPermission = myGroup.AddPermission(MiniProgramsPermissions.UserInfo.Default, L("Permission:UserInfo"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MiniProgramsResource>(name);
        }
    }
}
