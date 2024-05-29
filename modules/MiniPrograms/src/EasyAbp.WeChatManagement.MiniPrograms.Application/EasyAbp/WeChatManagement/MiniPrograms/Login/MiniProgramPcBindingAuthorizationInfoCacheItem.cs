using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class MiniProgramPcBindingAuthorizationInfoCacheItem
    {
        /// <summary>
        /// 小程序 AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        ///  wx.login 调用后返回的 code 的值
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}
