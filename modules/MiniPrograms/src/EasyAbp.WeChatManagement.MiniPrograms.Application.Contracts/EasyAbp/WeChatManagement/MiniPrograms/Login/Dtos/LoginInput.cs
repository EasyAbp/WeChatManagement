using System.ComponentModel.DataAnnotations;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    public class LoginInput
    {
        /// <summary>
        /// 小程序的 appid
        /// </summary>
        [Required]
        public string AppId { get; set; }

        /// <summary>
        /// wx.login 调用后返回的 code 的值
        /// 2021年2月23日起，若小程序已在微信开放平台进行绑定，则通过wx.login接口获取的登录凭证可直接换取unionID
        /// https://developers.weixin.qq.com/community/develop/doc/000cacfa20ce88df04cb468bc52801
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 查找并使用最近一次登录的租户登录（忽略当前租户环境）
        /// </summary>
        public bool LookupUseRecentlyTenant { get; set; }
    }
}