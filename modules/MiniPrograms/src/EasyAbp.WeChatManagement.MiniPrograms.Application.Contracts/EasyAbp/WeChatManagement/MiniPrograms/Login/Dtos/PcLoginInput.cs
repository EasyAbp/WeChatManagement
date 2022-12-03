using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    [Serializable]
    public class PcLoginInput
    {
        [Required]
        public string Token { get; set; }

        public string Scope { get; set; }
    }
}