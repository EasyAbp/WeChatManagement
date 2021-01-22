using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class MiniProgramLoginMatchNoUserException : BusinessException
    {
        public MiniProgramLoginMatchNoUserException() : base("MiniProgramLoginMatchNoUser", "请先创建账号")
        {
            
        }
    }
}