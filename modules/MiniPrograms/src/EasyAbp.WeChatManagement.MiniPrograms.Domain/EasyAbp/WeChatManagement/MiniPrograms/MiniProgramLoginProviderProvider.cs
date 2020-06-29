using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class MiniProgramLoginProviderProvider : IMiniProgramLoginProviderProvider, ITransientDependency
    {
        public virtual string WeChatAppLoginProviderPrefix { get; } = "WeChatApp:";
        public virtual string WeChatOpenLoginProviderPrefix { get; } = "WeChatOpen:";
        
        public virtual Task<string> GetAppLoginProviderAsync(MiniProgram miniProgram)
        {
            Check.NotNullOrWhiteSpace(miniProgram.AppId, nameof(miniProgram.AppId));

            return Task.FromResult(WeChatAppLoginProviderPrefix + miniProgram.AppId);
        }

        public virtual Task<string> GetOpenLoginProviderAsync(MiniProgram miniProgram)
        {
            Check.NotNullOrWhiteSpace(miniProgram.OpenAppId, nameof(miniProgram.OpenAppId));
            
            return Task.FromResult(WeChatOpenLoginProviderPrefix + miniProgram.OpenAppId);
        }
    }
}