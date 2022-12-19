using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services.Response;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Fakes;

public class FakeThirdPartyPlatformWeService : ThirdPartyPlatformWeService
{
    public const string PreAuthCode = "my_pre_auth_code";
    
    public const string MiniProgramAuthorizerAppId = "wx326eecacf7370d4e";
    public const string MiniProgramAuthorizationCode = "my_miniprogram_authorization_code";
    private const string MiniProgramAccessToken = "mini_program_access_token";
    private const string MiniProgramRefreshToken = "mini_program_refresh_token";

    public const string OfficialAuthorizerAppId = "wxf8b4f85f3a794e77";
    public const string OfficialAuthorizationCode = "my_official_authorization_code";
    private const string OfficialAccessToken = "official_access_token";
    private const string OfficialRefreshToken = "official_refresh_token";

    public static Dictionary<string, string> AuthorizerAppIds = new()
    {
        { ThirdPartyPlatformsTestConsts.AuthorizationCode, ThirdPartyPlatformsTestConsts.AuthorizerAppId },
        { MiniProgramAuthorizationCode, MiniProgramAuthorizerAppId },
        { OfficialAuthorizationCode, OfficialAuthorizerAppId },
    };

    public static Dictionary<string, string> AccessTokens = new()
    {
        { ThirdPartyPlatformsTestConsts.AuthorizerAppId, ThirdPartyPlatformsTestConsts.AuthorizerAccessToken },
        { MiniProgramAuthorizerAppId, MiniProgramAccessToken },
        { OfficialAuthorizerAppId, OfficialAccessToken },
    };

    public static Dictionary<string, string> RefreshTokens = new()
    {
        { ThirdPartyPlatformsTestConsts.AuthorizerAppId, ThirdPartyPlatformsTestConsts.AuthorizerRefreshToken },
        { MiniProgramAuthorizerAppId, MiniProgramRefreshToken },
        { OfficialAuthorizerAppId, OfficialRefreshToken },
    };

    public FakeThirdPartyPlatformWeService(AbpWeChatThirdPartyPlatformOptions options,
        IAbpLazyServiceProvider lazyServiceProvider) : base(options, lazyServiceProvider)
    {
    }

    public override Task<QueryAuthResponse> QueryAuthAsync(string authorizationCode)
    {
        return GetQueryAuthResponseAsync(AuthorizerAppIds[authorizationCode]);
    }

    private Task<QueryAuthResponse> GetQueryAuthResponseAsync(string authorizerAppId)
    {
        return Task.FromResult(new QueryAuthResponse
        {
            ErrorMessage = null,
            ErrorCode = 0,
            AuthorizationInfo = new QueryAuthResponseAuthorizationInfo
            {
                AuthorizerAppId = authorizerAppId,
                AuthorizerAccessToken = AccessTokens[authorizerAppId],
                ExpiresIn = 6000,
                AuthorizerRefreshToken = RefreshTokens[authorizerAppId],
                FuncInfo = new List<QueryAuthResponseFuncInfoItem>
                {
                    new()
                    {
                        FuncScopeCategory = new QueryAuthResponseFuncScopeCategory
                        {
                            Id = 5
                        }
                    },
                    new()
                    {
                        FuncScopeCategory = new QueryAuthResponseFuncScopeCategory
                        {
                            Id = 8
                        }
                    }
                }
            }
        });
    }

    public override Task<PreAuthCodeResponse> GetPreAuthCodeAsync()
    {
        return Task.FromResult(new PreAuthCodeResponse
        {
            ErrorMessage = null,
            ErrorCode = 0,
            PreAuthCode = PreAuthCode,
            ExpiresIn = 600
        });
    }
}