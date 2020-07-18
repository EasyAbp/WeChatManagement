using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    [Serializable]
    public class AuthorizePcInput
    {
        [Required]
        public string Token { get; set; }
    }
}