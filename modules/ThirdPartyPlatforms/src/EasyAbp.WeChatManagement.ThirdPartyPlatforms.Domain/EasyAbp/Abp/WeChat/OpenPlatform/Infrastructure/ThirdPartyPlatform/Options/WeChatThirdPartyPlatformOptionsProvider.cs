using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options;

public class WeChatThirdPartyPlatformOptionsProvider : IWeChatThirdPartyPlatformOptionsProvider, ITransientDependency
{
    private readonly IWeChatAppRepository _weChatAppRepository;

    public WeChatThirdPartyPlatformOptionsProvider(IWeChatAppRepository weChatAppRepository)
    {
        _weChatAppRepository = weChatAppRepository;
    }

    public virtual async Task<IWeChatThirdPartyPlatformOptions> GetAsync(string appId)
    {
        var weChatApp = await _weChatAppRepository.GetThirdPartyPlatformAppByAppIdAsync(appId);

        return new AbpWeChatThirdPartyPlatformOptions
        {
            Token = weChatApp.Token,
            AppId = weChatApp.AppId,
            AppSecret = weChatApp.AppSecret,
            EncodingAesKey = weChatApp.EncodingAesKey
        };
    }
}