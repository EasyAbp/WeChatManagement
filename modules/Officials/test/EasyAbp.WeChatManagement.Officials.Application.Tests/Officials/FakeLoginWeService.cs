using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Options;
using EasyAbp.Abp.WeChat.Official.Services.Login;
using EasyAbp.WeChatManagement.Officials.Login;
using Volo.Abp.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace EasyAbp.WeChatManagement.Officials.Officials;

[ExposeServices(typeof(LoginWeService), typeof(FakeLoginWeService))]
[Dependency(ReplaceServices = true)]
public class FakeLoginWeService : LoginWeService
{
    public FakeLoginWeService(AbpWeChatOfficialOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(
        options, lazyServiceProvider)
    {
    }

    public override Task<Code2AccessTokenResponse> Code2AccessTokenAsync(string appId, string appSecret, string jsCode,
        string grantType = "authorization_code")
    {
        return Task.FromResult(new Code2AccessTokenResponse
        {
            ErrorMessage = null,
            ErrorCode = 0,
            OpenId = "MyOpenId",
            AccessToken= "MyAccessToken",
            Scope=null,
            ExpiresIn=1,
            RefreshToken=null
        });
    }

    public override Task<Code2AccessTokenResponse> Code2AccessTokenAsync(string jsCode, string grantType = "authorization_code")
    {
        return Task.FromResult(new Code2AccessTokenResponse
        {
            ErrorMessage = null,
            ErrorCode = 0,
            OpenId = "MyOpenId",
            AccessToken = "MyAccessToken",
            Scope = null,
            ExpiresIn = 1,
            RefreshToken = null
        });
    }
}