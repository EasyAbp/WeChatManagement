using System;
using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public record LoginResultInfoModel
    {
        public MiniProgram MiniProgram { get; init; }
        
        public string LoginProvider { get; init; }
        
        public string ProviderKey { get; init; }
        
        public string UnionId { get; init; }

        public Code2SessionResponse Code2SessionResponse { get; init; }
    }
}