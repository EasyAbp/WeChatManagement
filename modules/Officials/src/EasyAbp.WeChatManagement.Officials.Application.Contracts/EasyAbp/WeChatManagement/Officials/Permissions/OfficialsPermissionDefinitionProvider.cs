using EasyAbp.WeChatManagement.Officials.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.WeChatManagement.Officials.Permissions
{
    public class OfficialsPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(OfficialsPermissions.GroupName, L("Permission:Officials"));
            
            var userInfoPermission = myGroup.AddPermission(OfficialsPermissions.UserInfo.Default, L("Permission:UserInfo"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<OfficialsResource>(name);
        }
    }
}
