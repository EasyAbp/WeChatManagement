using System;

namespace EasyAbp.WeChatManagement.Officials.Login
{
    public class OfficialPcLoginAuthorizationCacheItem
    {
        public string AppId { get; set; }
        
        public string UnionId { get; set; }
        
        public string OpenId { get; set; }
        
        public Guid UserId { get; set; }
    }
}