using Volo.Abp;

namespace EasyAbp.WeChatManagement.Officials
{
    public class OfficialLoginMatchNoUserException : BusinessException
    {
        public OfficialLoginMatchNoUserException() : base("OfficialLoginMatchNoUser", "请先创建账号")
        {
            
        }
    }
}