using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.ApiRequests;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.ComponentAccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Models;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Shouldly;
using Volo.Abp.Security.Encryption;
using Xunit;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthCallback;

public class AuthCallbackAppServiceTests : ThirdPartyPlatformsApplicationTestBase
{
    private const string ComponentAccessToken = "my_component_access_token";
    private const string AuthorizerName = "my_customer";

    private readonly IAuthorizationAppService _authorizationAppService;
    private readonly IAuthorizerSecretRepository _authorizerSecretRepository;
    private readonly IWeChatAppRepository _weChatAppRepository;
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IAuthorizerAccessTokenCache _authorizerAccessTokenCache;
    private IAbpWeChatServiceFactory _abpWeChatServiceFactory;

    public AuthCallbackAppServiceTests()
    {
        _authorizationAppService = GetRequiredService<IAuthorizationAppService>();
        _authorizerSecretRepository = GetRequiredService<IAuthorizerSecretRepository>();
        _weChatAppRepository = GetRequiredService<IWeChatAppRepository>();
        _stringEncryptionService = GetRequiredService<IStringEncryptionService>();
        _authorizerAccessTokenCache = GetRequiredService<IAuthorizerAccessTokenCache>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        base.AfterAddApplication(services);

        var componentAccessTokenProvider = Substitute.For<IComponentAccessTokenProvider>();
        services.Replace(ServiceDescriptor.Singleton(s => componentAccessTokenProvider));

        componentAccessTokenProvider.GetAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(ComponentAccessToken);

        var weChatThirdPartyPlatformApiRequester = Substitute.For<IWeChatThirdPartyPlatformApiRequester>();
        services.Replace(ServiceDescriptor.Transient(s => weChatThirdPartyPlatformApiRequester));

        weChatThirdPartyPlatformApiRequester.RequestAsync(Arg.Any<string>(), HttpMethod.Post,
                Arg.Is<GetAuthorizerInfoRequest>(x =>
                    x.ComponentAppId == ThirdPartyPlatformsTestConsts.AppId &&
                    x.AuthorizerAppId == FakeThirdPartyPlatformWeService.OfficialAuthorizerAppId),
                Arg.Is<IAbpWeChatOptions>(i => i.AppId == ThirdPartyPlatformsTestConsts.AppId))
            .Returns(
                "{\"authorizer_info\":{\"nick_name\":\"微信SDK Demo Special\",\"head_img\":\"http://wx.qlogo.cn/mmopen/GPy\",\"service_type_info\":{\"id\":2},\"verify_type_info\":{\"id\":0},\"user_name\":\"gh_eb5e3a772040\",\"principal_name\":\"腾讯计算机系统有限公司\",\"business_info\":{\"open_store\":0,\"open_scan\":0,\"open_pay\":0,\"open_card\":0,\"open_shake\":0},\"alias\":\"paytest01\",\"qrcode_url\":\"URL\",\"account_status\":1},\"authorization_info\":{\"authorizer_appid\":\"wxf8b4f85f3a794e77\",\"func_info\":[{\"funcscope_category\":{\"id\":1}},{\"funcscope_category\":{\"id\":2}}]}}");

        weChatThirdPartyPlatformApiRequester.RequestAsync(Arg.Any<string>(), HttpMethod.Post,
                Arg.Is<GetAuthorizerInfoRequest>(x =>
                    x.ComponentAppId == ThirdPartyPlatformsTestConsts.AppId &&
                    x.AuthorizerAppId == FakeThirdPartyPlatformWeService.MiniProgramAuthorizerAppId),
                Arg.Is<IAbpWeChatOptions>(i => i.AppId == ThirdPartyPlatformsTestConsts.AppId))
            .Returns(
                "{\"authorizer_info\":{\"nick_name\":\"美妆饰品\",\"head_img\":\"http://wx.qlogo.cn/mmopen/jJSbu4Te5ibibv2mJFb9ho1JuAfW9tyic5NX0Vhia4GMv3RDAdh3gTia6ewrbqYpn65UTxl7nT56nuiaM0dFnKVUEE83n2yH5cQStb/0\",\"service_type_info\":{\"id\":0},\"verify_type_info\":{\"id\":-1},\"user_name\":\"gh_c43395cb652e\",\"alias\":\"\",\"qrcode_url\":\"http://mmbiz.qpic.cn/mmbiz_jpg/kPxpXe3ic7TBGOvHkK1rGplicjachD5iaLic75NthsCZcd2CYoqkJAo7YPqEndQcSyCDNGXic7F00yWdhOFZGmmhe6g/0\",\"business_info\":{\"open_pay\":0,\"open_shake\":0,\"open_scan\":0,\"open_card\":0,\"open_store\":0},\"idc\":1,\"principal_name\":\"个人\",\"signature\":\"做美装，精美饰品等搭配教学\",\"MiniProgramInfo\":{\"network\":{\"RequestDomain\":[\"https://weixin.qq.com\"],\"WsRequestDomain\":[\"wss://weixin.qq.com\"],\"UploadDomain\":[\"https://weixin.qq.com\"],\"DownloadDomain\":[\"https://weixin.qq.com\"],\"BizDomain\":[],\"UDPDomain\":[],\"TCPDomain\":[],\"PrefetchDNSDomain\":[],\"NewRequestDomain\":[],\"NewWsRequestDomain\":[],\"NewUploadDomain\":[],\"NewDownloadDomain\":[],\"NewBizDomain\":[],\"NewUDPDomain\":[],\"NewTCPDomain\":[],\"NewPrefetchDNSDomain\":[]},\"categories\":[{\"first\":\"生活服务\",\"second\":\"丽人服务\"},{\"first\":\"旅游服务\",\"second\":\"旅游资讯\"},{\"first\":\"物流服务\",\"second\":\"查件\"}],\"visit_status\":0},\"register_type\":0,\"account_status\":1,\"basic_config\":{\"is_phone_configured\":true,\"is_email_configured\":true}},\"authorization_info\":{\"authorizer_appid\":\"wx326eecacf7370d4e\",\"authorizer_refresh_token\":\"refreshtoken@@@RU0Sgi7bD6apS7frS9gj8Sbws7OoDejK9Z-cm0EnCzg\",\"func_info\":[{\"funcscope_category\":{\"id\":3},\"confirm_info\":{\"need_confirm\":0,\"already_confirm\":0,\"can_confirm\":0}},{\"funcscope_category\":{\"id\":7}},{\"funcscope_category\":{\"id\":17}},{\"funcscope_category\":{\"id\":18},\"confirm_info\":{\"need_confirm\":0,\"already_confirm\":0,\"can_confirm\":0}},{\"funcscope_category\":{\"id\":19}},{\"funcscope_category\":{\"id\":30},\"confirm_info\":{\"need_confirm\":0,\"already_confirm\":0,\"can_confirm\":0}},{\"funcscope_category\":{\"id\":115}}]}}");

        _abpWeChatServiceFactory = Substitute.For<IAbpWeChatServiceFactory>();
        services.Replace(ServiceDescriptor.Transient(s => _abpWeChatServiceFactory));

        _abpWeChatServiceFactory.CreateAsync<ThirdPartyPlatformWeService>(ThirdPartyPlatformsTestConsts.AppId).Returns(
            new FakeThirdPartyPlatformWeService(new AbpWeChatThirdPartyPlatformOptions
            {
                AppId = ThirdPartyPlatformsTestConsts.AppId
            }, null));
    }

    [Fact]
    public async Task Should_Handle_Official_Callback()
    {
        await HandleCallbackAsync(FakeThirdPartyPlatformWeService.OfficialAuthorizerAppId,
            FakeThirdPartyPlatformWeService.OfficialAuthorizationCode);

        var authorizerWeChatApp = await _weChatAppRepository.FindAsync(x =>
            x.Type == WeChatAppType.Official && x.AppId == FakeThirdPartyPlatformWeService.OfficialAuthorizerAppId);

        authorizerWeChatApp.ShouldNotBeNull();
        authorizerWeChatApp.Name.ShouldBe("gh_eb5e3a772040");
        authorizerWeChatApp.DisplayName.ShouldBe("微信SDK Demo Special");
        authorizerWeChatApp.OpenAppIdOrName.ShouldBe($"3rd-party:{AuthorizerName}");
    }

    [Fact]
    public async Task Should_Handle_MiniProgram_Callback()
    {
        await HandleCallbackAsync(FakeThirdPartyPlatformWeService.MiniProgramAuthorizerAppId,
            FakeThirdPartyPlatformWeService.MiniProgramAuthorizationCode);

        var authorizerWeChatApp = await _weChatAppRepository.FindAsync(x =>
            x.Type == WeChatAppType.MiniProgram &&
            x.AppId == FakeThirdPartyPlatformWeService.MiniProgramAuthorizerAppId);

        authorizerWeChatApp.ShouldNotBeNull();
        authorizerWeChatApp.Name.ShouldBe("gh_c43395cb652e");
        authorizerWeChatApp.DisplayName.ShouldBe("美妆饰品");
        authorizerWeChatApp.OpenAppIdOrName.ShouldBe($"3rd-party:{AuthorizerName}");
    }

    protected async Task HandleCallbackAsync(string authorizerAppId, string authorizationCode)
    {
        var thirdPartyPlatformWeChatApp =
            await _weChatAppRepository.GetThirdPartyPlatformAppByAppIdAsync(ThirdPartyPlatformsTestConsts.AppId);

        var preAuthResult = await _authorizationAppService.PreAuthAsync(new PreAuthInputDto(
            thirdPartyPlatformWeChatApp.Id, AuthorizerName, true, true, null, null));

        var authorizerSecret = await _authorizerSecretRepository.FindAsync(x =>
            x.ComponentAppId == ThirdPartyPlatformsTestConsts.AppId &&
            x.AuthorizerAppId == authorizerAppId);

        authorizerSecret.ShouldBeNull();

        var input = new HandleCallbackInputDto(authorizationCode, preAuthResult.Token);

        (await _authorizationAppService.HandleCallbackAsync(input)).ErrorCode.ShouldBe(0);

        authorizerSecret = await _authorizerSecretRepository.FindAsync(x =>
            x.ComponentAppId == ThirdPartyPlatformsTestConsts.AppId &&
            x.AuthorizerAppId == authorizerAppId);

        authorizerSecret.ShouldNotBeNull();
        authorizerSecret.EncryptedRefreshToken.ShouldBe(
            _stringEncryptionService.Encrypt(FakeThirdPartyPlatformWeService.RefreshTokens[authorizerAppId]));
        authorizerSecret.CategoryIds.Count.ShouldBe(2);
        authorizerSecret.CategoryIds.ShouldContain(5);
        authorizerSecret.CategoryIds.ShouldContain(8);

        (await _authorizerAccessTokenCache.GetOrNullAsync(ThirdPartyPlatformsTestConsts.AppId,
            authorizerAppId)).ShouldBe(FakeThirdPartyPlatformWeService.AccessTokens[authorizerAppId]);
    }
}