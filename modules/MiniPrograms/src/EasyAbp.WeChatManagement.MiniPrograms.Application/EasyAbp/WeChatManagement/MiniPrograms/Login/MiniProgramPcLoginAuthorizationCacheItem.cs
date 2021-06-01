using System;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class MiniProgramPcLoginAuthorizationCacheItem
    {
        public string AppId { get; set; }
        
        public string UnionId { get; set; }
        
        public string OpenId { get; set; }
        
        public Guid UserId { get; set; }
    }
}