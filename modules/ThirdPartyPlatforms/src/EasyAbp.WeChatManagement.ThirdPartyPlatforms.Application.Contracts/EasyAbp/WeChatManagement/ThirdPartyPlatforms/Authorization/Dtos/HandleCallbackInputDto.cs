using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;

[Serializable]
public class HandleCallbackInputDto
{
    /// <summary>
    /// 微信第三方平台授权流程中获得的 AuthorizationCode
    /// </summary>
    [NotNull]
    public string AuthorizationCode { get; set; } = null!;

    /// <summary>
    /// 微信管理模块生成的 token
    /// </summary>
    [NotNull]
    public string Token { get; set; } = null!;

    public HandleCallbackInputDto()
    {
    }

    public HandleCallbackInputDto(
        [NotNull] string authorizationCode,
        [NotNull] string token)
    {
        AuthorizationCode = Check.NotNullOrWhiteSpace(authorizationCode, nameof(authorizationCode));
        Token = Check.NotNullOrWhiteSpace(token, nameof(token));
    }
}