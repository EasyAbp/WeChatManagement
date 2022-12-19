using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;

[Serializable]
public class PreAuthInputDto
{
    /// <summary>
    /// 微信第三方平台的 WeChatAppId
    /// </summary>
    public Guid ThirdPartyPlatformWeChatAppId { get; set; }

    /// <summary>
    /// 本属性将用于区别不同的授权方，设置 OpenAppIdOrName = "3rd-party:" + AuthorizerName，
    /// 最终将不同的开放平台区别开来，合成 IdentityUserLogin 实体的 ProviderName，
    /// 相同 AuthorizerName 的不同 WeChatApp 会自动绑定在相同 UnionId 对应的 IdentityUserLogin 中。
    /// 你需要做的，仅是为客户使用唯一的 AuthorizerName。
    /// </summary>
    [NotNull]
    public string AuthorizerName { get; set; } = null!;

    public bool AllowOfficial { get; set; }

    public bool AllowMiniProgram { get; set; }

    [CanBeNull]
    public string SpecifiedAppId { get; set; }

    [CanBeNull]
    public string CategoryIds { get; set; }

    public PreAuthInputDto()
    {
    }

    public PreAuthInputDto(Guid thirdPartyPlatformWeChatAppId, [NotNull] string authorizerName, bool allowOfficial,
        bool allowMiniProgram, [CanBeNull] string specifiedAppId, [CanBeNull] string categoryIds)
    {
        ThirdPartyPlatformWeChatAppId = thirdPartyPlatformWeChatAppId;
        AuthorizerName = Check.NotNullOrWhiteSpace(authorizerName, nameof(authorizerName));
        AllowOfficial = allowOfficial;
        AllowMiniProgram = allowMiniProgram;
        SpecifiedAppId = specifiedAppId;
        CategoryIds = categoryIds;
    }
}