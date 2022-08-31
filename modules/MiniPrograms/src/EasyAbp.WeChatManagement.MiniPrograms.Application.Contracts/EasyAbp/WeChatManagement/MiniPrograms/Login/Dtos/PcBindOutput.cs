using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    public class PcBindOutput
    {
        /// <summary>
        /// 扫码过期
        /// </summary>
        public bool Expired { get; set; }
        /// <summary>
        /// 成功扫码
        /// </summary>
        public bool IsSucess { get; set; }
        /// <summary>
        /// 成功绑定
        /// </summary>
        public bool HasBound { get; set; }/*
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }*/
    }
}
