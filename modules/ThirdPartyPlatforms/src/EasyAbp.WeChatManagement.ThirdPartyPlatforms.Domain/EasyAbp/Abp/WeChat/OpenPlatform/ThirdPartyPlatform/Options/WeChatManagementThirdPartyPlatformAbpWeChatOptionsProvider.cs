using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.WeChatManagement.Common.WeChatApps;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;

public class WeChatManagementThirdPartyPlatformAbpWeChatOptionsProvider :
    AbpWeChatOptionsProviderBase<AbpWeChatThirdPartyPlatformOptions>
{
    private readonly IWeChatAppRepository _weChatAppRepository;

    public WeChatManagementThirdPartyPlatformAbpWeChatOptionsProvider(IWeChatAppRepository weChatAppRepository)
    {
        _weChatAppRepository = weChatAppRepository;
    }

    public override async Task<AbpWeChatThirdPartyPlatformOptions> GetAsync(string appId)
    {
        var weChatApp = await _weChatAppRepository.GetThirdPartyPlatformAppByAppIdAsync(appId);

        return new AbpWeChatThirdPartyPlatformOptions
        {
            AppId = weChatApp.AppId,
            AppSecret = weChatApp.AppSecret,
            EncodingAesKey = weChatApp.EncodingAesKey,
            Token = weChatApp.Token
        };
    }
}