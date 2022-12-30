using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.WeChatManagement.Common.WeChatApps
{
    public static class WeChatAppRepositoryExtensions
    {
        public static Task<WeChatApp> GetOfficialAppAsync(this IWeChatAppRepository weChatAppRepository, Guid id)
        {
            return weChatAppRepository.GetAsync(x => x.Id == id && x.Type == WeChatAppType.Official);
        }
        
        public static Task<WeChatApp> GetOfficialAppByAppIdAsync(this IWeChatAppRepository weChatAppRepository,
            [NotNull] string appId)
        {
            return weChatAppRepository.GetAsync(x => x.AppId == appId && x.Type == WeChatAppType.Official);
        }

        public static Task<WeChatApp> GetOfficialAppByNameAsync(this IWeChatAppRepository weChatAppRepository,
            [NotNull] string name)
        {
            return weChatAppRepository.GetAsync(x => x.Name == name && x.Type == WeChatAppType.Official);
        }

        public static Task<WeChatApp> FindOfficialAppAsync(this IWeChatAppRepository weChatAppRepository, Guid id)
        {
            return weChatAppRepository.FindAsync(x => x.Id == id && x.Type == WeChatAppType.Official);
        }
        
        public static Task<WeChatApp> FindOfficialAppByAppIdAsync(this IWeChatAppRepository weChatAppRepository,
            [NotNull] string appId)
        {
            return weChatAppRepository.FindAsync(x => x.AppId == appId && x.Type == WeChatAppType.Official);
        }
        
        public static Task<WeChatApp> FindOfficialAppByNameAsync(this IWeChatAppRepository weChatAppRepository,
            [NotNull] string name)
        {
            return weChatAppRepository.FindAsync(x => x.Name == name && x.Type == WeChatAppType.Official);
        }
    }
}