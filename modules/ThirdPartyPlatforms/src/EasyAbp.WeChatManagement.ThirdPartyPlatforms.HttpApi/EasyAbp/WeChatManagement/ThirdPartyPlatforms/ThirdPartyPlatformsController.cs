using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

[Area(WeChatManagementRemoteServiceConsts.RemoteServiceName)]
public abstract class ThirdPartyPlatformsController : AbpControllerBase
{
    protected ThirdPartyPlatformsController()
    {
        LocalizationResource = typeof(ThirdPartyPlatformsResource);
    }
}
