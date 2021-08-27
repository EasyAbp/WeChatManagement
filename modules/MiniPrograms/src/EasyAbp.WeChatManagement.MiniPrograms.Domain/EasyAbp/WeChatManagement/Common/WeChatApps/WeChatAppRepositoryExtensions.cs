using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.WeChatManagement.Common.WeChatApps
{
    public static class WeChatAppRepositoryExtensions
    {
        public static Task<WeChatApp> GetMiniProgramAppAsync(this IWeChatAppRepository weChatAppRepository, Guid id)
        {
            return weChatAppRepository.GetAsync(x => x.Id == id && x.Type == WeChatAppType.MiniProgram);
        }
        
        public static Task<WeChatApp> GetMiniProgramAppByAppIdAsync(this IWeChatAppRepository weChatAppRepository,
            [NotNull] string appId)
        {
            return weChatAppRepository.GetAsync(x => x.AppId == appId && x.Type == WeChatAppType.MiniProgram);
        }

        public static Task<WeChatApp> GetMiniProgramAppByNameAsync(this IWeChatAppRepository weChatAppRepository,
            [NotNull] string name)
        {
            return weChatAppRepository.GetAsync(x => x.Name == name && x.Type == WeChatAppType.MiniProgram);
        }

        public static Task<WeChatApp> FindMiniProgramAppAsync(this IWeChatAppRepository weChatAppRepository, Guid id)
        {
            return weChatAppRepository.FindAsync(x => x.Id == id && x.Type == WeChatAppType.MiniProgram);
        }
        
        public static Task<WeChatApp> FindMiniProgramAppByAppIdAsync(this IWeChatAppRepository weChatAppRepository,
            [NotNull] string appId)
        {
            return weChatAppRepository.FindAsync(x => x.AppId == appId && x.Type == WeChatAppType.MiniProgram);
        }
        
        public static Task<WeChatApp> FindMiniProgramAppByNameAsync(this IWeChatAppRepository weChatAppRepository,
            [NotNull] string name)
        {
            return weChatAppRepository.FindAsync(x => x.Name == name && x.Type == WeChatAppType.MiniProgram);
        }
    }
}