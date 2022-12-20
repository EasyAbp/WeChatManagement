using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Models;

public class GetAuthorizerInfoRequest : OpenPlatformCommonRequest
{
    [JsonProperty("component_appid")]
    public string ComponentAppId { get; set; }

    [JsonProperty("authorizer_appid")]
    public string AuthorizerAppId { get; set; }
}