using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class MiniProgramLoginProviderProvider : IMiniProgramLoginProviderProvider, ITransientDependency
    {
        public virtual string WeChatAppLoginProviderPrefix { get; } = "WeChatApp:";
        public virtual string WeChatOpenLoginProviderPrefix { get; } = "WeChatOpen:";
        
        public virtual Task<string> GetAppLoginProviderAsync(WeChatApp miniProgram)
        {
            Check.NotNullOrWhiteSpace(miniProgram.AppId, nameof(miniProgram.AppId));

            return Task.FromResult(WeChatAppLoginProviderPrefix + miniProgram.AppId);
        }

        public virtual Task<string> GetOpenLoginProviderAsync(WeChatApp miniProgram)
        {
            Check.NotNullOrWhiteSpace(miniProgram.OpenAppIdOrName, nameof(miniProgram.OpenAppIdOrName));
            
            return Task.FromResult(WeChatOpenLoginProviderPrefix + miniProgram.OpenAppIdOrName);
        }
    }
}