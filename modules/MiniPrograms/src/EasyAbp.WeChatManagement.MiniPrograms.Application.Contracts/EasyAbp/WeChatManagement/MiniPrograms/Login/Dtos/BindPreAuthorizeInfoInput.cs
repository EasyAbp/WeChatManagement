using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    [Serializable]
    public class BindPreAuthorizeInfoInput
    {
        [Required]
        public string Token { get; set; }
    }
}
