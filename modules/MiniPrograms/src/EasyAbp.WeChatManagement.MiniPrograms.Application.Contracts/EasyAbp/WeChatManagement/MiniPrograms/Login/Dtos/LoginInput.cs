using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;

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
        /// 通过微信开放能力 getRealtimePhoneNumber 获取的动态令牌
        /// 当通过 openId/unionId 查找用户失败时，如提供此值，应用服务将使用该令牌作为备选方案：
        /// 1. 尝试换取用户手机号
        /// 2. 通过手机号查找已注册用户
        /// 3. 若找到匹配用户，则直接使用该用户登录；若未找到，则创建新用户并关联此手机号
        /// https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/getRealtimePhoneNumber.html
        /// </summary>
        [CanBeNull]
        public string PhoneNumberCode { get; set; }

        /// <summary>
        /// 查找并使用最近一次登录的租户登录（忽略当前租户环境）
        /// </summary>
        public bool LookupUseRecentlyTenant { get; set; }

        public string Scope { get; set; }
    }
}