using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp.Security.Encryption;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;

public class WeChatManagementThirdPartyPlatformAbpWeChatOptionsProvider :
    AbpWeChatOptionsProviderBase<AbpWeChatThirdPartyPlatformOptions>
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IWeChatAppRepository _weChatAppRepository;

    public WeChatManagementThirdPartyPlatformAbpWeChatOptionsProvider(
        IStringEncryptionService stringEncryptionService,
        IWeChatAppRepository weChatAppRepository)
    {
        _stringEncryptionService = stringEncryptionService;
        _weChatAppRepository = weChatAppRepository;
    }

    public override async Task<AbpWeChatThirdPartyPlatformOptions> GetAsync(string appId)
    {
        var weChatApp = await _weChatAppRepository.GetThirdPartyPlatformAppByAppIdAsync(appId);

        return new AbpWeChatThirdPartyPlatformOptions
        {
            AppId = weChatApp.AppId,
            AppSecret = _stringEncryptionService.Decrypt(weChatApp.EncryptedAppSecret),
            EncodingAesKey = weChatApp.EncodingAesKey,
            Token = weChatApp.Token
        };
    }
}