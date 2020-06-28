using WeChatManagementSample.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace WeChatManagementSample.Permissions
{
    public class WeChatManagementSamplePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(WeChatManagementSamplePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(WeChatManagementSamplePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<WeChatManagementSampleResource>(name);
        }
    }
}
