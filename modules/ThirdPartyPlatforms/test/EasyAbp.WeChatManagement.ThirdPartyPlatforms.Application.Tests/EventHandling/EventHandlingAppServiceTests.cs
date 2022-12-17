using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;
using EasyAbp.Abp.WeChat.OpenPlatform.EventHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.Options.OptionsResolving;
using EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform.Response;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using Volo.Abp.Security.Encryption;
using Xunit;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EventHandling;

public class EventHandlingAppServiceTests : ThirdPartyPlatformsApplicationTestBase
{
    private const string TimeStamp = "1413192605";
    private const string InfoType = "component_verify_ticket";
    private const string Nonce = "some_nonce";
    private const string AuthorizationCode = "my_authorization_code";
    private const string PreAuthCode = "my_pre_auth_code";
    private const string NewAuthorizerAccessToken = "new_authorizer_access_token";
    private const string NewAuthorizerRefreshToken = "new_authorizer_refresh_token";

    private readonly IWeChatAppRepository _weChatAppRepository;
    private readonly IAuthorizerSecretRepository _authorizerSecretRepository;
    private readonly IEventHandlingAppService _eventHandlingAppService;
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IAuthorizerAccessTokenCache _authorizerAccessTokenCache;
    private ThirdPartyPlatformApiService _thirdPartyPlatformApiService;

    public EventHandlingAppServiceTests()
    {
        _weChatAppRepository = GetRequiredService<IWeChatAppRepository>();
        _authorizerSecretRepository = GetRequiredService<IAuthorizerSecretRepository>();
        _eventHandlingAppService = GetRequiredService<IEventHandlingAppService>();
        _stringEncryptionService = GetRequiredService<IStringEncryptionService>();
        _authorizerAccessTokenCache = GetRequiredService<IAuthorizerAccessTokenCache>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        base.AfterAddApplication(services);
        _thirdPartyPlatformApiService = Substitute.For<ThirdPartyPlatformApiService>(
            new WeChatThirdPartyPlatformOptionsResolver(RootServiceProvider,
                Options.Create(new AbpWeChatThirdPartyPlatformResolvingOptions())));
        _thirdPartyPlatformApiService.QueryAuthAsync(AuthorizationCode).Returns(new QueryAuthResponse
        {
            ErrorMessage = null,
            ErrorCode = 0,
            AuthorizationInfo = new QueryAuthResponseAuthorizationInfo
            {
                AuthorizerAppId = ThirdPartyPlatformsTestConsts.AuthorizerAppId,
                AuthorizerAccessToken = NewAuthorizerAccessToken,
                ExpiresIn = 6000,
                AuthorizerRefreshToken = NewAuthorizerRefreshToken,
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
        services.Replace(ServiceDescriptor.Transient(s => _thirdPartyPlatformApiService));
    }

    [Fact]
    public async Task Should_Save_Verify_Ticket()
    {
        var crypt = new WXBizMsgCrypt(
            ThirdPartyPlatformsTestConsts.Token,
            ThirdPartyPlatformsTestConsts.EncodingAesKey,
            ThirdPartyPlatformsTestConsts.AppId);

        string encryptedMsg = null;

        crypt.EncryptMsg(
            $"<xml>" +
            $"<AppId>{ThirdPartyPlatformsTestConsts.AppId}</AppId>" +
            $"<CreateTime>{TimeStamp}</CreateTime>" +
            $"<InfoType>{InfoType}</InfoType>" +
            $"<ComponentVerifyTicket>{ThirdPartyPlatformsTestConsts.ComponentVerifyTicket}</ComponentVerifyTicket>" +
            $"</xml>",
            TimeStamp, Nonce, ref encryptedMsg).ShouldBe(0);

        var xml = XDocument.Parse(encryptedMsg);

        (await _eventHandlingAppService.NotifyAuthAsync(ThirdPartyPlatformsTestConsts.AppId,
            new WeChatEventNotificationRequestModel
            {
                PostData = encryptedMsg,
                MsgSignature = xml.Element("xml")!.Element("MsgSignature")!.Value,
                Timestamp = TimeStamp,
                Notice = Nonce
            })).Success.ShouldBeTrue();

        var weChatApp =
            await _weChatAppRepository.GetThirdPartyPlatformAppByAppIdAsync(ThirdPartyPlatformsTestConsts.AppId);

        weChatApp.GetVerifyTicketOrNullAsync(_stringEncryptionService)
            .ShouldBe(ThirdPartyPlatformsTestConsts.ComponentVerifyTicket);
    }

    [Fact]
    public async Task Should_Update_Refresh_Token()
    {
        var authorizerSecret = await _authorizerSecretRepository.GetAsync(x =>
            x.ComponentAppId == ThirdPartyPlatformsTestConsts.AppId &&
            x.AuthorizerAppId == ThirdPartyPlatformsTestConsts.AuthorizerAppId);

        authorizerSecret.EncryptedRefreshToken.ShouldBe(
            _stringEncryptionService.Encrypt(ThirdPartyPlatformsTestConsts.RefreshToken));

        var crypt = new WXBizMsgCrypt(
            ThirdPartyPlatformsTestConsts.Token,
            ThirdPartyPlatformsTestConsts.EncodingAesKey,
            ThirdPartyPlatformsTestConsts.AppId);

        string encryptedMsg = null;

        crypt.EncryptMsg(
            $"<xml>" +
            $"<AppId>{ThirdPartyPlatformsTestConsts.AppId}</AppId>" +
            $"<CreateTime>{TimeStamp}</CreateTime>" +
            $"<InfoType>updateauthorized</InfoType>" +
            $"<AuthorizerAppid>{ThirdPartyPlatformsTestConsts.AuthorizerAppId}</AuthorizerAppid>" +
            $"<AuthorizationCode>{AuthorizationCode}</AuthorizationCode>" +
            $"<AuthorizationCodeExpiredTime>6000</AuthorizationCodeExpiredTime>" +
            $"<PreAuthCode>{PreAuthCode}</PreAuthCode>" +
            $"</xml>",
            TimeStamp, Nonce, ref encryptedMsg).ShouldBe(0);

        var xml = XDocument.Parse(encryptedMsg);

        (await _eventHandlingAppService.NotifyAuthAsync(ThirdPartyPlatformsTestConsts.AppId,
            new WeChatEventNotificationRequestModel
            {
                PostData = encryptedMsg,
                MsgSignature = xml.Element("xml")!.Element("MsgSignature")!.Value,
                Timestamp = TimeStamp,
                Notice = Nonce
            })).Success.ShouldBeTrue();

        authorizerSecret = await _authorizerSecretRepository.GetAsync(x =>
            x.ComponentAppId == ThirdPartyPlatformsTestConsts.AppId &&
            x.AuthorizerAppId == ThirdPartyPlatformsTestConsts.AuthorizerAppId);

        authorizerSecret.ShouldNotBeNull();
        authorizerSecret.EncryptedRefreshToken.ShouldBe(_stringEncryptionService.Encrypt(NewAuthorizerRefreshToken));
        authorizerSecret.CategoryIds.Count.ShouldBe(2);
        authorizerSecret.CategoryIds.ShouldContain(5);
        authorizerSecret.CategoryIds.ShouldContain(8);
    }

    [Fact]
    public async Task Should_Delete_AuthorizerSecret_Entity()
    {
        var authorizerSecret = await _authorizerSecretRepository.FindAsync(x =>
            x.ComponentAppId == ThirdPartyPlatformsTestConsts.AppId &&
            x.AuthorizerAppId == ThirdPartyPlatformsTestConsts.AuthorizerAppId);

        authorizerSecret.ShouldNotBeNull();

        var crypt = new WXBizMsgCrypt(
            ThirdPartyPlatformsTestConsts.Token,
            ThirdPartyPlatformsTestConsts.EncodingAesKey,
            ThirdPartyPlatformsTestConsts.AppId);

        string encryptedMsg = null;

        crypt.EncryptMsg(
            $"<xml>" +
            $"<AppId>{ThirdPartyPlatformsTestConsts.AppId}</AppId>" +
            $"<CreateTime>{TimeStamp}</CreateTime>" +
            $"<InfoType>unauthorized</InfoType>" +
            $"<AuthorizerAppid>{ThirdPartyPlatformsTestConsts.AuthorizerAppId}</AuthorizerAppid>" +
            $"</xml>",
            TimeStamp, Nonce, ref encryptedMsg).ShouldBe(0);

        var xml = XDocument.Parse(encryptedMsg);

        (await _eventHandlingAppService.NotifyAuthAsync(ThirdPartyPlatformsTestConsts.AppId,
            new WeChatEventNotificationRequestModel
            {
                PostData = encryptedMsg,
                MsgSignature = xml.Element("xml")!.Element("MsgSignature")!.Value,
                Timestamp = TimeStamp,
                Notice = Nonce
            })).Success.ShouldBeTrue();

        authorizerSecret = await _authorizerSecretRepository.FindAsync(x =>
            x.ComponentAppId == ThirdPartyPlatformsTestConsts.AppId &&
            x.AuthorizerAppId == ThirdPartyPlatformsTestConsts.AuthorizerAppId);

        authorizerSecret.ShouldBeNull();

        (await _authorizerAccessTokenCache.GetAsync(ThirdPartyPlatformsTestConsts.AppId,
            ThirdPartyPlatformsTestConsts.AuthorizerAppId)).ShouldBe(null);
    }
}