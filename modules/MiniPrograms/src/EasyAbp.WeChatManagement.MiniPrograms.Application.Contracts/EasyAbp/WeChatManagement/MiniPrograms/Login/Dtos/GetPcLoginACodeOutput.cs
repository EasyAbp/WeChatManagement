using System;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    [Serializable]
    public class GetPcLoginACodeOutput
    {
        public string Token { get; set; }
        
        public byte[] ACode { get; set; }
    }
}