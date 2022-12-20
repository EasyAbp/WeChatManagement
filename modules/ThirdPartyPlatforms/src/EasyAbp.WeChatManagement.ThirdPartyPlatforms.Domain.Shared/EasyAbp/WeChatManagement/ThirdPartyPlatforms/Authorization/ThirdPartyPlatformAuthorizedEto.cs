using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization;

[Serializable]
public class ThirdPartyPlatformAuthorizedEto : IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid ThirdPartyPlatformWeChatAppId { get; set; }

    public Guid AuthorizerWeChatAppId { get; set; }

    public string ComponentAppId { get; set; }

    public string AuthorizerAppId { get; set; }

    public string AuthorizerName { get; set; }

    public string CategoryIds { get; set; }
}