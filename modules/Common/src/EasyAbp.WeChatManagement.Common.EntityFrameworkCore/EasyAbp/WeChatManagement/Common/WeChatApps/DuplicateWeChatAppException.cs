using Volo.Abp;

namespace EasyAbp.WeChatManagement.Common.WeChatApps
{
    public class DuplicateWeChatAppException : BusinessException
    {
        public DuplicateWeChatAppException() : base("DuplicateWeChatApp", "重复的微信应用")
        {
            
        }
    }
}