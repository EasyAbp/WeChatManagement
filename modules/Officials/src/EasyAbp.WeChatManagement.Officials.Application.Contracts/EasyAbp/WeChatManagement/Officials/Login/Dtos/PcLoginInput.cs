using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.Officials.Login.Dtos
{
    [Serializable]
    public class PcLoginInput
    {
        [Required]
        public string Token { get; set; }
    }
}