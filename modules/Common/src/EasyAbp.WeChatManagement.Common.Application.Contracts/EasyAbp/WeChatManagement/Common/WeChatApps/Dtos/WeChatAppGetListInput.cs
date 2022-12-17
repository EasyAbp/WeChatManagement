using System;

namespace EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;

[Serializable]
public class WeChatAppGetListInput
{
    public WeChatAppType? Type { get; set; }

    public Guid? ComponentWeChatAppId { get; set; }

    public string OpenAppIdOrName { get; set; }

    public string AppId { get; set; }

    public bool? IsStatic { get; set; }
}