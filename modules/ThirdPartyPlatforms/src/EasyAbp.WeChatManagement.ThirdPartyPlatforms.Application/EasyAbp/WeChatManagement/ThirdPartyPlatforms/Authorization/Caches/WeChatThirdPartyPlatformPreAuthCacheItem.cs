using System;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Caches;

public class WeChatThirdPartyPlatformPreAuthCacheItem
{
    public Guid ThirdPartyPlatformWeChatAppId { get; set; }

    public string AuthorizerName { get; set; } = null!;

    public bool AllowOfficial { get; set; }

    public bool AllowMiniProgram { get; set; }

    public string SpecifiedAppId { get; set; }

    public string CategoryIds { get; set; }
}