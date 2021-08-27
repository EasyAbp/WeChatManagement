using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public interface IMiniProgramLoginProviderProvider
    {
        string WeChatAppLoginProviderPrefix { get; }
        string WeChatOpenLoginProviderPrefix { get; }
        
        Task<string> GetAppLoginProviderAsync(WeChatApp miniProgram);
        
        Task<string> GetOpenLoginProviderAsync(WeChatApp miniProgram);
    }
}