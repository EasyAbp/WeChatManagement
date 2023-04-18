using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    public class BindInput : LoginInput
    {
        /// <summary>
        /// 绑定用户缓存标识
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// 强制绑定
        /// </summary>
        public bool ForceBinding { get; set; }
    }
}
