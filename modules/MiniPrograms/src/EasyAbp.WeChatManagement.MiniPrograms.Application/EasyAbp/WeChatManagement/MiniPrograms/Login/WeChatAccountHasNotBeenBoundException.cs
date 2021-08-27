using System;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class WeChatAccountHasNotBeenBoundException : BusinessException
    {
        public WeChatAccountHasNotBeenBoundException(
            string message = null,
            string code = "WeChatAccountHasNotBeenBound",
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(message, code, details, innerException, logLevel)
        {
        }
    }
}