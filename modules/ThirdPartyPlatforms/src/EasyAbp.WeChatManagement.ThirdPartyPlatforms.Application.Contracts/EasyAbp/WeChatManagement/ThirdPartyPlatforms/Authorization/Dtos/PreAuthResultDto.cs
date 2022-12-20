using System;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;

[Serializable]
public class PreAuthResultDto
{
    public string PreAuthCode { get; set; }

    public string Token { get; set; }
}