using EasyAbp.Abp.WeChat.Official.Services.Login;
using EasyAbp.WeChatManagement.Common.WeChatApps;

namespace EasyAbp.WeChatManagement.Officials.Login
{
    public record LoginResultInfoModel
    {
        public WeChatApp Official { get; init; }
        
        public string LoginProvider { get; init; }
        
        public string ProviderKey { get; init; }

        public Code2AccessTokenResponse Code2AccessTokenResponse { get; init; }
    }
}