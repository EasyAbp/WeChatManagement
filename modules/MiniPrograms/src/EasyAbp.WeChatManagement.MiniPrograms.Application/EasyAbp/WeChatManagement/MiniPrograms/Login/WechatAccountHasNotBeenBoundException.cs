using System;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class WechatAccountHasNotBeenBoundException : UserFriendlyException
    {
        public WechatAccountHasNotBeenBoundException(
            string message = "账号尚未绑定微信",
            string code = "WechatAccountHasNotBeenBound",
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(message, code, details, innerException, logLevel)
        {
        }
    }
}