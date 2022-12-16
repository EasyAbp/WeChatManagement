using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;

[Serializable]
public class HandleCallbackInputDto
{
    /// <summary>
    /// 微信第三方平台的 WeChatAppId
    /// </summary>
    public Guid ThirdPartyPlatformWeChatAppId { get; set; }

    /// <summary>
    /// 微信第三方平台授权流程中获得的 PreAuthCode
    /// </summary>
    [NotNull]
    public string PreAuthCode { get; set; } = null!;

    /// <summary>
    /// 微信第三方平台授权流程中获得的 AuthorizationCode
    /// </summary>
    [NotNull]
    public string AuthorizationCode { get; set; } = null!;

    public HandleCallbackInputDto()
    {
    }

    public HandleCallbackInputDto(
        Guid thirdPartyPlatformWeChatAppId,
        [NotNull] string preAuthCode,
        [NotNull] string authorizationCode)
    {
        ThirdPartyPlatformWeChatAppId = thirdPartyPlatformWeChatAppId;
        PreAuthCode = Check.NotNullOrWhiteSpace(preAuthCode, nameof(preAuthCode));
        AuthorizationCode = Check.NotNullOrWhiteSpace(authorizationCode, nameof(authorizationCode));
    }
}