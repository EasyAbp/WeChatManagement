using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;

public class WeChatManagementThirdPartyPlatformAbpWeChatOptionsProvider : ThirdPartyPlatformAbpWeChatOptionsProvider
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IWeChatAppRepository _weChatAppRepository;

    public WeChatManagementThirdPartyPlatformAbpWeChatOptionsProvider(
        IStringEncryptionService stringEncryptionService,
        IWeChatAppRepository weChatAppRepository,
        ISettingProvider settingProvider) : base(settingProvider)
    {
        _stringEncryptionService = stringEncryptionService;
        _weChatAppRepository = weChatAppRepository;
    }

    public override async Task<AbpWeChatThirdPartyPlatformOptions> GetAsync(string appId)
    {
        if (appId.IsNullOrWhiteSpace())
        {
            return await base.GetAsync(appId);
        }

        var weChatApp = await _weChatAppRepository.GetThirdPartyPlatformAppByAppIdAsync(appId);

        return new AbpWeChatThirdPartyPlatformOptions
        {
            AppId = weChatApp.AppId,
            AppSecret = _stringEncryptionService.Decrypt(weChatApp.EncryptedAppSecret),
            Token = _stringEncryptionService.Decrypt(weChatApp.EncryptedToken),
            EncodingAesKey = _stringEncryptionService.Decrypt(weChatApp.EncryptedEncodingAesKey)
        };
    }
}