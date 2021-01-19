using System;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class WechatAccountHasBeenBoundException : UserFriendlyException
    {
        public WechatAccountHasBeenBoundException(
            string message = null,
            string code = "WechatAccountHasBeenBound",
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(message, code, details, innerException, logLevel)
        {
        }
    }
}