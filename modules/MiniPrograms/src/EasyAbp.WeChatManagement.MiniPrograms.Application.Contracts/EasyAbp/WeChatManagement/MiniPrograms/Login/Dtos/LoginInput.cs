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
        /// </summary>
        [Required]
        public string Code { get; set; }
        
        /// <summary>
        /// wx.getUserInfo 调用后返回的 userInfo 的值
        /// </summary>
        public UserInfoModel UserInfo { get; set; }
        
        /// <summary>
        /// wx.getUserInfo 调用后返回的 rawData 的值
        /// </summary>
        [Required]
        public string RawData { get; set; }
        
        /// <summary>
        /// wx.getUserInfo 调用后返回的 signature 的值
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// wx.getUserInfo 调用后返回的 encryptedData 的值
        /// </summary>
        /// <remark>
        /// 若不传入此值，可能导致无法获取用户的 unionId，
        /// 一般情况下建议传值，符合以下“任一”条件可不传入：
        /// 1. 明确：此用户（在当前小程序加入开放平台后）曾授权登录过“当前”小程序，且登录时使用的是本接口。
        /// 2. 明确：此用户在同开放平台+同主体下，关注过“任意”公众号。
        /// 3. 明确：此用户在同开放平台+同主体下，登录过“其他任意” app。
        /// 4. 明确：当前 app 没有加入开放平台，或不需要使用 UnionId 作为用户识别凭证。
        /// 更多信息：https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/union-id.html
        /// </remark>
        public string EncryptedData { get; set; }
        
        /// <summary>
        /// wx.getUserInfo 调用后返回的 iv 的值
        /// </summary>
        /// <remark>
        /// 若不传入此值，可能导致无法获取用户的 unionId，
        /// 一般情况下建议传值，符合以下“任一”条件可不传入：
        /// 1. 明确：此用户（在当前小程序加入开放平台后）曾授权登录过“当前”小程序，且登录时使用的是本接口。
        /// 2. 明确：此用户在同开放平台+同主体下，关注过“任意”公众号。
        /// 3. 明确：此用户在同开放平台+同主体下，登录过“其他任意” app。
        /// 4. 明确：当前 app 没有加入开放平台，或不需要使用 UnionId 作为用户识别凭证。
        /// 更多信息：https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/union-id.html
        /// </remark>
        public string Iv { get; set; }
        
        /// <summary>
        /// 查找并使用最近一次登录的租户登录（忽略当前租户环境）
        /// </summary>
        public bool LookupUseRecentlyTenant { get; set; }
    }
}