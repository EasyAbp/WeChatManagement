using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Permissions;

public class ThirdPartyPlatformsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ThirdPartyPlatformsPermissions.GroupName, L("Permission:ThirdPartyPlatforms"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ThirdPartyPlatformsResource>(name);
    }
}
