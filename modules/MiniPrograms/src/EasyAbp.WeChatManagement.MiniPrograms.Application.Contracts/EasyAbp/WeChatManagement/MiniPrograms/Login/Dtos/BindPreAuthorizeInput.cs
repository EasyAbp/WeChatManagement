using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    public class BindPreAuthorizeInput : LoginInput
    {
        [Required]
        public string Token { get; set; }
        /// <summary>
        /// 微信昵称（预留参数）
        /// </summary>
        public string NickName { get; set; } = "微信用户";
        /// <summary>
        /// 用户头像（预留参数）
        /// </summary>
        public string AvatarUrl { get; set; } = "https://thirdwx.qlogo.cn/mmopen/vi_32/POgEwh4mIHO4nibH0KlMECNjjGxQUq24ZEaGT4poC6icRiccVGKSyXwibcPq4BWmiaIGuG1icwxaQX6grC9VemZoJ8rg/132";
    }
}
