using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public class DuplicateMiniProgramException : BusinessException
    {
        public DuplicateMiniProgramException() : base("DuplicateMiniProgram", "重复的小程序")
        {
            
        }
    }
}