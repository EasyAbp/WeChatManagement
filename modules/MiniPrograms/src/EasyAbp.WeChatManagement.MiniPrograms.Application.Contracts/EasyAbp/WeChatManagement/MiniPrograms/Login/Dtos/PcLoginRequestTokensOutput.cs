using System;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    [Serializable]
    public class PcLoginRequestTokensOutput : PcLoginOutput
    {
        public string RawData { get; set; }
    }
}