using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.Officials.Login.Dtos
{
    public class LoginInput
    {
        /// <summary>
        /// 公众号的 appid
        /// </summary>
        [Required]
        public string AppId { get; set; }

        /// <summary>
        /// 回调后返回的 code 的值
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 查找并使用最近一次登录的租户登录（忽略当前租户环境）
        /// </summary>
        public bool LookupUseRecentlyTenant { get; set; }
    }
}