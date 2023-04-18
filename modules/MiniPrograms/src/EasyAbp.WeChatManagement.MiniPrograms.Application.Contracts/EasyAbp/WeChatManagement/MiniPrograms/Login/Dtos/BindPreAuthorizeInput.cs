using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    public class BindPreAuthorizeInput : LoginInput
    {
        [Required]
        public string Token { get; set; }
        /// <summary>
        /// 微信昵称（预留参数）
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户头像（预留参数）
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}
