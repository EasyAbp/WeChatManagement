using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.MiniProgram.Options;

public class WeChatManagementMiniProgramAbpWeChatOptionsProvider : MiniProgramAbpWeChatOptionsProvider
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IWeChatAppRepository _weChatAppRepository;

    public WeChatManagementMiniProgramAbpWeChatOptionsProvider(
        IStringEncryptionService stringEncryptionService,
        IWeChatAppRepository weChatAppRepository,
        ISettingProvider settingProvider) : base(settingProvider)
    {
        _stringEncryptionService = stringEncryptionService;
        _weChatAppRepository = weChatAppRepository;
    }

    public override async Task<AbpWeChatMiniProgramOptions> GetAsync(string appId)
    {
        if (appId.IsNullOrWhiteSpace())
        {
            return await base.GetAsync(appId);
        }

        var weChatApp = await _weChatAppRepository.GetMiniProgramAppByAppIdAsync(appId);

        return new AbpWeChatMiniProgramOptions
        {
            AppId = weChatApp.AppId,
            AppSecret = _stringEncryptionService.Decrypt(weChatApp.EncryptedAppSecret),
            Token = _stringEncryptionService.Decrypt(weChatApp.EncryptedToken),
            EncodingAesKey = _stringEncryptionService.Decrypt(weChatApp.EncryptedEncodingAesKey)
        };
    }
}