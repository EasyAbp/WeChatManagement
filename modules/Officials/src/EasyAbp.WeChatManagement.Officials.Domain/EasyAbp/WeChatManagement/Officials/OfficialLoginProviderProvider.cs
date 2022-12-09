using EasyAbp.WeChatManagement.Common.WeChatApps;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.Officials
{
    public class OfficialLoginProviderProvider : IOfficialLoginProviderProvider, ITransientDependency
    {
        public virtual string WeChatAppLoginProviderPrefix { get; } = "WeChatApp:";
        public virtual string WeChatOpenLoginProviderPrefix { get; } = "WeChatOpen:";

        public virtual Task<string> GetAppLoginProviderAsync(WeChatApp official)
        {
            Check.NotNullOrWhiteSpace(official.AppId, nameof(official.AppId));

            return Task.FromResult(WeChatAppLoginProviderPrefix + official.AppId);
        }

        public virtual Task<string> GetOpenLoginProviderAsync(WeChatApp official)
        {
            Check.NotNullOrWhiteSpace(official.OpenAppIdOrName, nameof(official.OpenAppIdOrName));

            return Task.FromResult(WeChatOpenLoginProviderPrefix + official.OpenAppIdOrName);
        }
    }
}