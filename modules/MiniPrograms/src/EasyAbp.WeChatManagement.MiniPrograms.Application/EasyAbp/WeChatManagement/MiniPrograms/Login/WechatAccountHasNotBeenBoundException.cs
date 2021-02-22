using System;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class WechatAccountHasNotBeenBoundException : BusinessException
    {
        public WechatAccountHasNotBeenBoundException(
            string message = null,
            string code = "WechatAccountHasNotBeenBound",
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(message, code, details, innerException, logLevel)
        {
        }
    }
}