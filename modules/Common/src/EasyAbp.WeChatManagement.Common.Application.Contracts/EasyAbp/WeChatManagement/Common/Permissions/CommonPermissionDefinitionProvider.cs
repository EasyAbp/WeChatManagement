using EasyAbp.WeChatManagement.Common.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.WeChatManagement.Common.Permissions
{
    public class CommonPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CommonPermissions.GroupName, L("Permission:Common"));
            
            var weChatAppPermission = myGroup.AddPermission(CommonPermissions.WeChatApp.Default, L("Permission:WeChatApp"));
            weChatAppPermission.AddChild(CommonPermissions.WeChatApp.Create, L("Permission:Create"));
            weChatAppPermission.AddChild(CommonPermissions.WeChatApp.Update, L("Permission:Update"));
            weChatAppPermission.AddChild(CommonPermissions.WeChatApp.Delete, L("Permission:Delete"));

            var weChatAppUserPermission = myGroup.AddPermission(CommonPermissions.WeChatAppUser.Default, L("Permission:WeChatAppUser"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CommonResource>(name);
        }
    }
}