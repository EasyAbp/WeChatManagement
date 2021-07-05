using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    public class UnbindUserInput
    {
        /// <summary>
        /// 小程序的 appid
        /// </summary>
        [Required]
        public string AppId { get; set; }

        /// <summary>
        /// 要解绑的用户ID。不传则解绑自己
        /// </summary>
        public Guid? UserId { get; set; }
    }
}
