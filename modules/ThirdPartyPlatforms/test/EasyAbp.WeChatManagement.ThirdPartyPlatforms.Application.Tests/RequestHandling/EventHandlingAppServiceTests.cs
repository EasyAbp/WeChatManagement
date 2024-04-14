using System.Threading.Tasks;
using System.Xml.Linq;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Shouldly;
using Volo.Abp.Security.Encryption;
using Xunit;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.RequestHandling;

public class EventHandlingAppServiceTests : ThirdPartyPlatformsApplicationTestBase
{
    private const string TimeStamp = "1413192605";
    private const string InfoType = "component_verify_ticket";
    private const string Nonce = "some_nonce";
    private const string PreAuthCode = "my_pre_auth_code";

    private readonly IWeChatAppRepository _weChatAppRepository;
    private readonly IAuthorizerSecretRepository _authorizerSecretRepository;
    private readonly IEventHandlingAppService _eventHandlingAppService;
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IAuthorizerAccessTokenCache _authorizerAccessTokenCache;
    private IAbpWeChatServiceFactory _abpWeChatServiceFactory;

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

        _abpWeChatServiceFactory = Substitute.For<IAbpWeChatServiceFactory>();
        services.Replace(ServiceDescriptor.Transient(s => _abpWeChatServiceFactory));

        _abpWeChatServiceFactory.CreateAsync<ThirdPartyPlatformWeService>(ThirdPartyPlatformsTestConsts.AppId).Returns(
            new FakeThirdPartyPlatformWeService(new AbpWeChatThirdPartyPlatformOptions
            {
                AppId = ThirdPartyPlatformsTestConsts.AppId
            }, null));
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

        (await _eventHandlingAppService.NotifyAuthAsync(new NotifyAuthInput
        {
            ComponentAppId = ThirdPartyPlatformsTestConsts.AppId,
            EventRequest = new WeChatEventRequestModel
            {
                PostData = encryptedMsg,
                MsgSignature = xml.Element("xml")!.Element("MsgSignature")!.Value,
                Timestamp = TimeStamp,
                Nonce = Nonce
            }
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
            _stringEncryptionService.Encrypt(ThirdPartyPlatformsTestConsts.AuthorizerRefreshToken));

        FakeThirdPartyPlatformWeService.RefreshTokens[ThirdPartyPlatformsTestConsts.AuthorizerAppId] = "new_token";

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
            $"<AuthorizationCode>{ThirdPartyPlatformsTestConsts.AuthorizationCode}</AuthorizationCode>" +
            $"<AuthorizationCodeExpiredTime>6000</AuthorizationCodeExpiredTime>" +
            $"<PreAuthCode>{PreAuthCode}</PreAuthCode>" +
            $"</xml>",
            TimeStamp, Nonce, ref encryptedMsg).ShouldBe(0);

        var xml = XDocument.Parse(encryptedMsg);

        (await _eventHandlingAppService.NotifyAuthAsync(new NotifyAuthInput
        {
            ComponentAppId = ThirdPartyPlatformsTestConsts.AppId,
            EventRequest = new WeChatEventRequestModel
            {
                PostData = encryptedMsg,
                MsgSignature = xml.Element("xml")!.Element("MsgSignature")!.Value,
                Timestamp = TimeStamp,
                Nonce = Nonce
            }
        })).Success.ShouldBeTrue();

        authorizerSecret = await _authorizerSecretRepository.GetAsync(x =>
            x.ComponentAppId == ThirdPartyPlatformsTestConsts.AppId &&
            x.AuthorizerAppId == ThirdPartyPlatformsTestConsts.AuthorizerAppId);

        authorizerSecret.ShouldNotBeNull();
        authorizerSecret.EncryptedRefreshToken.ShouldBe(_stringEncryptionService.Encrypt("new_token"));
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

        (await _eventHandlingAppService.NotifyAuthAsync(new NotifyAuthInput
        {
            ComponentAppId = ThirdPartyPlatformsTestConsts.AppId,
            EventRequest = new WeChatEventRequestModel
            {
                PostData = encryptedMsg,
                MsgSignature = xml.Element("xml")!.Element("MsgSignature")!.Value,
                Timestamp = TimeStamp,
                Nonce = Nonce
            }
        })).Success.ShouldBeTrue();

        authorizerSecret = await _authorizerSecretRepository.FindAsync(x =>
            x.ComponentAppId == ThirdPartyPlatformsTestConsts.AppId &&
            x.AuthorizerAppId == ThirdPartyPlatformsTestConsts.AuthorizerAppId);

        authorizerSecret.ShouldBeNull();

        (await _authorizerAccessTokenCache.GetOrNullAsync(ThirdPartyPlatformsTestConsts.AppId,
            ThirdPartyPlatformsTestConsts.AuthorizerAppId)).ShouldBe(null);
    }
}