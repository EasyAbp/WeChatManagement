using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.WeChatManagement.Common.WeChatApps;

namespace EasyAbp.Abp.WeChat.MiniProgram.Options;

public class WeChatManagementMiniProgramAbpWeChatOptionsProvider :
    AbpWeChatOptionsProviderBase<AbpWeChatMiniProgramOptions>
{
    private readonly IWeChatAppRepository _weChatAppRepository;

    public WeChatManagementMiniProgramAbpWeChatOptionsProvider(IWeChatAppRepository weChatAppRepository)
    {
        _weChatAppRepository = weChatAppRepository;
    }

    public override async Task<AbpWeChatMiniProgramOptions> GetAsync(string appId)
    {
        var weChatApp = await _weChatAppRepository.GetMiniProgramAppByAppIdAsync(appId);

        return new AbpWeChatMiniProgramOptions
        {
            AppId = weChatApp.AppId,
            AppSecret = weChatApp.AppSecret,
            EncodingAesKey = weChatApp.EncodingAesKey,
            Token = weChatApp.Token
        };
    }
}