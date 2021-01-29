using System;
using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public record LoginResultInfoModel
    {
        public Guid? TenantId { get; init; }
        
        public string LoginProvider { get; init; }
        
        public string ProviderKey { get; init; }
        
        public string UnionId { get; init; }

        public Code2SessionResponse Code2SessionResponse { get; init; }
    }
}