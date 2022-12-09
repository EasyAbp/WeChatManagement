using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;

namespace EasyAbp.WeChatManagement.Officials
{
    public interface IOfficialLoginProviderProvider
    {
        string WeChatAppLoginProviderPrefix { get; }
        string WeChatOpenLoginProviderPrefix { get; }

        Task<string> GetAppLoginProviderAsync(WeChatApp official);

        Task<string> GetOpenLoginProviderAsync(WeChatApp official);
    }
}
