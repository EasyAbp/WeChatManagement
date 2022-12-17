using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Models;

public class GetAuthorizerInfoRequest : IOpenPlatformRequest
{
    [JsonProperty("component_appid")]
    public string ComponentAppId { get; set; }

    [JsonProperty("authorizer_appid")]
    public string AuthorizerAppId { get; set; }
}