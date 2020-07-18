using Volo.Abp;

namespace EasyAbp.WeChatManagement.Common
{
    public class WeChatBusinessException : BusinessException
    {
        public WeChatBusinessException(int errorCode, string errorMessage) : base(message: $"WeChat error: [{errorCode}] {errorMessage}")
        {
            
        }
    }
}