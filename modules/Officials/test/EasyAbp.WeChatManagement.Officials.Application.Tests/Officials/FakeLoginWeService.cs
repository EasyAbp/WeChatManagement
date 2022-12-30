using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Options;
using EasyAbp.Abp.WeChat.Official.Services.Login;
using EasyAbp.WeChatManagement.Officials.Login;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.Officials.Officials;

[ExposeServices(typeof(LoginWeService), typeof(FakeLoginWeService))]
[Dependency(ReplaceServices = true)]
public class FakeLoginWeService : LoginWeService
{
    public FakeLoginWeService(AbpWeChatOfficialOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(
        options, lazyServiceProvider)
    {
    }

    public override Task<Code2SessionResponse> Code2SessionAsync(string appId, string appSecret, string jsCode,
        string grantType = "authorization_code")
    {
        return Task.FromResult(new Code2SessionResponse
        {
            ErrorMessage = null,
            ErrorCode = 0,
            OpenId = "MyOpenId",
            SessionKey = "MySessionKey",
            UnionId = "MyUnionId"
        });
    }

    public override Task<Code2SessionResponse> Code2SessionAsync(string jsCode, string grantType = "authorization_code")
    {
        return Task.FromResult(new Code2SessionResponse
        {
            ErrorMessage = null,
            ErrorCode = 0,
            OpenId = "MyOpenId",
            SessionKey = "MySessionKey",
            UnionId = "MyUnionId"
        });
    }
}