using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public interface IMiniProgramLoginProviderProvider
    {
        string WeChatAppLoginProviderPrefix { get; }
        string WeChatOpenLoginProviderPrefix { get; }
        
        Task<string> GetAppLoginProviderAsync(MiniProgram miniProgram);
        
        Task<string> GetOpenLoginProviderAsync(MiniProgram miniProgram);
    }
}