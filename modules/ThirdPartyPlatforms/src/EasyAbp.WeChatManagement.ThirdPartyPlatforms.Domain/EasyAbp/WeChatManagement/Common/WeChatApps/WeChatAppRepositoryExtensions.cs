using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.WeChatManagement.Common.WeChatApps;

public static class WeChatAppRepositoryExtensions
{
    public static Task<WeChatApp> GetThirdPartyPlatformAppAsync(
        this IWeChatAppRepository weChatAppRepository, Guid id)
    {
        return weChatAppRepository.GetAsync(x => x.Id == id && x.Type == WeChatAppType.ThirdPartyPlatform);
    }

    public static Task<WeChatApp> GetThirdPartyPlatformAppByAppIdAsync(
        this IWeChatAppRepository weChatAppRepository, [NotNull] string appId)
    {
        return weChatAppRepository.GetAsync(x => x.AppId == appId && x.Type == WeChatAppType.ThirdPartyPlatform);
    }

    public static Task<WeChatApp> GetThirdPartyPlatformAppByNameAsync(
        this IWeChatAppRepository weChatAppRepository, [NotNull] string name)
    {
        return weChatAppRepository.GetAsync(x => x.Name == name && x.Type == WeChatAppType.ThirdPartyPlatform);
    }

    public static Task<WeChatApp> FindThirdPartyPlatformAppAsync(
        this IWeChatAppRepository weChatAppRepository, Guid id)
    {
        return weChatAppRepository.FindAsync(x => x.Id == id && x.Type == WeChatAppType.ThirdPartyPlatform);
    }

    public static Task<WeChatApp> FindThirdPartyPlatformAppByAppIdAsync(
        this IWeChatAppRepository weChatAppRepository, [NotNull] string appId)
    {
        return weChatAppRepository.FindAsync(x => x.AppId == appId && x.Type == WeChatAppType.ThirdPartyPlatform);
    }

    public static Task<WeChatApp> FindThirdPartyPlatformAppByNameAsync(
        this IWeChatAppRepository weChatAppRepository, [NotNull] string name)
    {
        return weChatAppRepository.FindAsync(x => x.Name == name && x.Type == WeChatAppType.ThirdPartyPlatform);
    }
}