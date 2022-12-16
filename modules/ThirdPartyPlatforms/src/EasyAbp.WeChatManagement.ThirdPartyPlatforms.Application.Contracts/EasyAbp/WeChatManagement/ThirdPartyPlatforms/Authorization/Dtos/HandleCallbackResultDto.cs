using System;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;

[Serializable]
public class HandleCallbackResultDto
{
    public int ErrorCode { get; set; }

    public string ErrorMessage { get; set; }
}