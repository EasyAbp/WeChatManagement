using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class WechatAccountNotBoundException : UserFriendlyException
    {
        public WechatAccountNotBoundException(
            string message = "微信账号未绑定,请先登录",
            string code = "WechatAccountNotBound",
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(message, code, details, innerException, logLevel)
        {
        }
    }
}
