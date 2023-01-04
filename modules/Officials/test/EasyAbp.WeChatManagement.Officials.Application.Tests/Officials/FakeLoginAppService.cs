using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using EasyAbp.WeChatManagement.Officials.Login;
using EasyAbp.WeChatManagement.Officials.UserInfos;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Security.Encryption;

namespace EasyAbp.WeChatManagement.Officials.Officials;

[ExposeServices(typeof(LoginAppService), typeof(FakeLoginAppService), typeof(ILoginAppService))]
[Dependency(ReplaceServices = true)]
public class FakeLoginAppService : LoginAppService
{
    public FakeLoginAppService(AbpSignInManager signInManager, IDataFilter dataFilter, IConfiguration configuration,
        IHttpClientFactory httpClientFactory, IUserInfoRepository userInfoRepository,
        IWeChatAppRepository weChatAppRepository, IStringEncryptionService stringEncryptionService,
        IWeChatAppUserRepository weChatAppUserRepository, IAbpWeChatServiceFactory abpWeChatServiceFactory,
        IOfficialLoginNewUserCreator OfficialLoginNewUserCreator,
        IOfficialLoginProviderProvider OfficialLoginProviderProvider,
        IOptions<IdentityOptions> identityOptions, IdentityUserManager identityUserManager) : base(signInManager,
        dataFilter, configuration, httpClientFactory, userInfoRepository, weChatAppRepository, stringEncryptionService,
        weChatAppUserRepository, abpWeChatServiceFactory, OfficialLoginNewUserCreator,
        OfficialLoginProviderProvider, identityOptions,
        identityUserManager)
    {
    }

    protected override Task<TokenResponse> RequestAuthServerLoginAsync(
        string appId, string unionId, string openId, string scope)
    {
        return Task.FromResult(new TokenResponse());
    }
}