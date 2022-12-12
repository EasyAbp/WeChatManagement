﻿using Volo.Abp.Reflection;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Permissions;

public class ThirdPartyPlatformsPermissions
{
    public const string GroupName = "EasyAbp.WeChatManagement.ThirdPartyPlatforms";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ThirdPartyPlatformsPermissions));
    }
}
