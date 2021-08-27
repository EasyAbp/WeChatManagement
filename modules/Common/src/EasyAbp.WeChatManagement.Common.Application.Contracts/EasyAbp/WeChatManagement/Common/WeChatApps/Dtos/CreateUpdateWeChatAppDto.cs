using System;

namespace EasyAbp.WeChatManagement.Common.WeChatApps.Dtos
{
    [Serializable]
    public class CreateUpdateWeChatAppDto
    {
        public WeChatAppType Type { get; set; }

        public Guid? WeChatComponentId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string OpenAppIdOrName { get; set; }
        
        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string Token { get; set; }

        public string EncodingAesKey { get; set; }
    }
}