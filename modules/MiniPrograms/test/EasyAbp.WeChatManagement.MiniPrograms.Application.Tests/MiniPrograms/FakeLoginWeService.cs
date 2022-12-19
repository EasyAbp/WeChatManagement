using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Options;
using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;

[ExposeServices(typeof(LoginWeService), typeof(FakeLoginWeService))]
[Dependency(ReplaceServices = true)]
public class FakeLoginWeService : LoginWeService
{
    public FakeLoginWeService(AbpWeChatMiniProgramOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(
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