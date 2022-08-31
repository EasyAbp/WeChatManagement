using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    [Serializable]
    public class PcBindInput
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public int Times { get; set; }
    }
}
