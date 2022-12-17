using EasyAbp.WeChatManagement.Common.WeChatApps;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Models;

public class AuthorizerInfoModel
{
    /// <summary>
    /// 原始 ID
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; }

    /// <summary>
    /// 应用类型
    /// </summary>
    public WeChatAppType WeChatAppType { get; set; }

    /// <summary>
    /// 查询结果 JSON 原文
    /// </summary>
    public string RawData { get; set; }
}