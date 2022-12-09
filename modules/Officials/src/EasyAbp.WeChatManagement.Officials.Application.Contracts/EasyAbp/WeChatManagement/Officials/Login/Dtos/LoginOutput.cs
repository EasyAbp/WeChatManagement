using System;

namespace EasyAbp.WeChatManagement.Officials.Login.Dtos
{
    public class LoginOutput
    {
        public Guid? TenantId { get; set; }
        
        public string RawData { get; set; }
    }
}