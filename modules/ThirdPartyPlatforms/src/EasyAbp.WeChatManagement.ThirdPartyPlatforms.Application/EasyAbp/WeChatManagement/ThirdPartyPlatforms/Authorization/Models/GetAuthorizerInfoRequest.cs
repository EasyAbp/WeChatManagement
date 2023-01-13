using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Models;

public class GetAuthorizerInfoRequest : OpenPlatformCommonRequest
{
    [JsonPropertyName("component_appid")]
    [JsonProperty("component_appid")]
    public string ComponentAppId { get; set; }

    [JsonPropertyName("authorizer_appid")]
    [JsonProperty("authorizer_appid")]
    public string AuthorizerAppId { get; set; }
}