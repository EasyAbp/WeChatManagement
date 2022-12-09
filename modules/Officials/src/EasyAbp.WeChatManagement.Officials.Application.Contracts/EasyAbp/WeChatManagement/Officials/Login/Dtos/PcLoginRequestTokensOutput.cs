using System;

namespace EasyAbp.WeChatManagement.Officials.Login.Dtos
{
    [Serializable]
    public class PcLoginRequestTokensOutput : PcLoginOutput
    {
        public string RawData { get; set; }
    }
}