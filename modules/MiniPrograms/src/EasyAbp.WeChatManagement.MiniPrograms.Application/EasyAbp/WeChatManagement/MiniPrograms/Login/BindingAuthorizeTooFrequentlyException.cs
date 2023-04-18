using System;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class BindingAuthorizeTooFrequentlyException : UserFriendlyException
    {
        public BindingAuthorizeTooFrequentlyException(
            string message = "BindingAuthorizeTooFrequently",
            string code = "BindingAuthorizeTooFrequently",
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning) : base(message, code, details, innerException, logLevel) { }
    }
}
