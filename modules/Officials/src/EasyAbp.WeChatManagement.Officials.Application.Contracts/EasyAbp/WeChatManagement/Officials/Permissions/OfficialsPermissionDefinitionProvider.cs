using EasyAbp.WeChatManagement.Officials.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.WeChatManagement.Officials.Permissions;

public class OfficialsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var officialsGroup = context.AddGroup(OfficialsPermissions.GroupName, L("Permission:Officials"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<OfficialsResource>(name);
    }
}
