using System;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class WeChatAccountHasBeenBoundException : BusinessException
    {
        public WeChatAccountHasBeenBoundException(
            string message = null,
            string code = "WeChatAccountHasBeenBound",
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(message, code, details, innerException, logLevel)
        {
        }
    }
}