using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp.Security.Encryption;

namespace EasyAbp.Abp.WeChat.MiniProgram.Options;

public class WeChatManagementMiniProgramAbpWeChatOptionsProvider :
    AbpWeChatOptionsProviderBase<AbpWeChatMiniProgramOptions>
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IWeChatAppRepository _weChatAppRepository;

    public WeChatManagementMiniProgramAbpWeChatOptionsProvider(
        IStringEncryptionService stringEncryptionService,
        IWeChatAppRepository weChatAppRepository)
    {
        _stringEncryptionService = stringEncryptionService;
        _weChatAppRepository = weChatAppRepository;
    }

    public override async Task<AbpWeChatMiniProgramOptions> GetAsync(string appId)
    {
        var weChatApp = await _weChatAppRepository.GetMiniProgramAppByAppIdAsync(appId);

        return new AbpWeChatMiniProgramOptions
        {
            AppId = weChatApp.AppId,
            AppSecret = _stringEncryptionService.Decrypt(weChatApp.EncryptedAppSecret),
            EncodingAesKey = weChatApp.EncodingAesKey,
            Token = weChatApp.Token
        };
    }
}