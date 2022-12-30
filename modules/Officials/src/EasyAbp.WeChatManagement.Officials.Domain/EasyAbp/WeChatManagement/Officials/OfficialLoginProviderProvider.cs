using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.Officials
{
    public class OfficialLoginProviderProvider : IOfficialLoginProviderProvider, ITransientDependency
    {
        public virtual string WeChatAppLoginProviderPrefix { get; } = "WeChatApp:";
        public virtual string WeChatOpenLoginProviderPrefix { get; } = "WeChatOpen:";
        
        public virtual Task<string> GetAppLoginProviderAsync(WeChatApp Official)
        {
            Check.NotNullOrWhiteSpace(Official.AppId, nameof(Official.AppId));

            return Task.FromResult(WeChatAppLoginProviderPrefix + Official.AppId);
        }

        public virtual Task<string> GetOpenLoginProviderAsync(WeChatApp Official)
        {
            Check.NotNullOrWhiteSpace(Official.OpenAppIdOrName, nameof(Official.OpenAppIdOrName));
            
            return Task.FromResult(WeChatOpenLoginProviderPrefix + Official.OpenAppIdOrName);
        }
    }
}