using EasyAbp.Abp.WeChat.Official;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve.Contributors;
using EasyAbp.Abp.WeChat.Official.Services.Login;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using EasyAbp.WeChatManagement.Officials.Login.Dtos;
using EasyAbp.WeChatManagement.Officials.Settings;
using EasyAbp.WeChatManagement.Officials.UserInfos;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace EasyAbp.WeChatManagement.Officials.Login
{
    public class LoginAppService : OfficialsAppService, ILoginAppService
    {
        protected virtual string BindPolicyName { get; set; }
        protected virtual string UnbindPolicyName { get; set; }

        private readonly LoginService _loginService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IDataFilter _dataFilter;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IWeChatOfficialAsyncLocal _weChatOfficialAsyncLocal;
        private readonly IWeChatAppRepository _weChatAppRepository;
        private readonly IWeChatAppUserRepository _weChatAppUserRepository;
        private readonly IOfficialLoginNewUserCreator _officialLoginNewUserCreator;
        private readonly IOfficialLoginProviderProvider _officialLoginProviderProvider;
        private readonly IDistributedCache<OfficialLoginAuthorizationCacheItem> _loginAuthorizationCache;
        private readonly IDistributedCache<OfficialLoginUserLimitCacheItem> _loginUserLimitCache;
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IdentityUserManager _identityUserManager;

        public LoginAppService(
            LoginService loginService,
            SignInManager<IdentityUser> signInManager,
            IDataFilter dataFilter,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IUserInfoRepository userInfoRepository,
            IWeChatOfficialAsyncLocal weChatOfficialAsyncLocal,
            IWeChatAppRepository weChatAppRepository,
            IWeChatAppUserRepository weChatAppUserRepository,
            IOfficialLoginNewUserCreator officialLoginNewUserCreator,
            IOfficialLoginProviderProvider officialLoginProviderProvider,
            IDistributedCache<OfficialLoginAuthorizationCacheItem> loginAuthorizationCache,
            IDistributedCache<OfficialLoginUserLimitCacheItem> loginUserLimitCache,
            IOptions<IdentityOptions> identityOptions,
            IdentityUserManager identityUserManager)
        {
            _loginService = loginService;
            _signInManager = signInManager;
            _dataFilter = dataFilter;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _userInfoRepository = userInfoRepository;
            _weChatOfficialAsyncLocal = weChatOfficialAsyncLocal;
            _weChatAppRepository = weChatAppRepository;
            _weChatAppUserRepository = weChatAppUserRepository;
            _officialLoginNewUserCreator = officialLoginNewUserCreator;
            _officialLoginProviderProvider = officialLoginProviderProvider;
            _loginAuthorizationCache = loginAuthorizationCache;
            _loginUserLimitCache = loginUserLimitCache;
            _identityOptions = identityOptions;
            _identityUserManager = identityUserManager;
        }

        public async Task LoginAsync(LoginInput input)
        {
            var loginResult = await GetLoginResultAsync(input);

            using var tenantChange = CurrentTenant.Change(loginResult.Official.TenantId);

            await _identityOptions.SetAsync();

            using (var uow = UnitOfWorkManager.Begin(new AbpUnitOfWorkOptions(true), true))
            {
                var identityUser =
                    await _identityUserManager.FindByLoginAsync(loginResult.LoginProvider, loginResult.ProviderKey) ??
                    await _officialLoginNewUserCreator.CreateAsync(loginResult.LoginProvider,
                        loginResult.ProviderKey);

                var weChatAppUser = await UpdateWeChatAppUserAsync(identityUser, loginResult.Official, loginResult.Code2AccessTokenResponse.OpenId);

                await TryCreateUserInfoAsync(identityUser, await GenerateFakeUserInfoAsync());

                await _loginAuthorizationCache.SetAsync(input.AppId,
                    new OfficialLoginAuthorizationCacheItem
                    {
                        AppId = input.AppId,
                        UnionId = weChatAppUser.UnionId,
                        OpenId = weChatAppUser.OpenId,
                        UserId = weChatAppUser.Id
                    },
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                    });

                await _signInManager.SignInAsync(identityUser, false);

                await uow.CompleteAsync();
            }
        }

        public async Task<GetLoginAuthorizeUrlOutput> GetLoginAuthorizeUrlAsync(string officialName, string handlePage = null)
        {
            var official = await _weChatAppRepository.GetOfficialAppByNameAsync(officialName);

            var options = new AbpWeChatOfficialOptions
            {
                Token = official.Token,
                AppId = official.AppId,
                AppSecret = official.AppSecret,
                OAuthRedirectUrl = official.OAuthRedirectUrl
            };

            using (_weChatOfficialAsyncLocal.Change(options))
            {
                if (handlePage.IsNullOrWhiteSpace())
                {
                    handlePage = await SettingProvider.GetOrNullAsync(OfficialsSettings.Login.HandlePage);
                }

                var authorizeUrl = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={official.AppId}&redirect_uri={HttpUtility.UrlEncode(official.OAuthRedirectUrl + "/Account/Login?method=WeChatOfficial&appId=" + official.AppId)}&response_type=code&scope=snsapi_base&state=#wechat_redirect";

                return new GetLoginAuthorizeUrlOutput
                {
                    HandlePage = handlePage,
                    AuthorizeUrl = authorizeUrl
                };
            }
        }

        protected virtual Task<UserInfoModel> GenerateFakeUserInfoAsync()
        {
            return Task.FromResult(new UserInfoModel
            {
                NickName = "微信用户",
                Gender = 0,
                Language = null,
                City = null,
                Province = null,
                Country = null,
                AvatarUrl = null,
            });
        }

        protected virtual async Task<TokenResponse> RequestIds4LoginAsync(string appId, string openId)
        {
            var client = _httpClientFactory.CreateClient(WeChatOfficialConsts.IdentityServerHttpClientName);

            var request = new TokenRequest
            {
                Address = _configuration["AuthServer:Authority"] + "/connect/token",
                GrantType = WeChatOfficialConsts.GrantType,

                ClientId = _configuration["AuthServer:ClientId"],
                ClientSecret = _configuration["AuthServer:ClientSecret"],

                Parameters =
                {
                    {"appid", appId},
                    {"openid", openId},
                }
            };

            request.Headers.Add(GetTenantHeaderName(), CurrentTenant.Id?.ToString());

            return await client.RequestTokenAsync(request);
        }

        protected virtual string GetTenantHeaderName()
        {
            return "__tenant";
        }

        protected virtual async Task<LoginResultInfoModel> GetLoginResultAsync(LoginInput input)
        {
            var tenantId = CurrentTenant.Id;
            var tenantChanged = false;

            WeChatApp official;

            if (input.LookupUseRecentlyTenant)
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    official = await _weChatAppRepository.FirstOrDefaultAsync(x =>
                        x.AppId == input.AppId && x.Type == WeChatAppType.Official);
                }
            }
            else
            {
                official = await _weChatAppRepository.GetOfficialAppByAppIdAsync(input.AppId);
            }

            var code2AccessTokenResponse =
                await _loginService.Code2SessionAsync(official.AppId, official.AppSecret, input.Code);

            var openId = code2AccessTokenResponse.OpenId;

            if (input.LookupUseRecentlyTenant)
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    tenantId = await _weChatAppUserRepository.FindRecentlyTenantIdAsync(input.AppId, openId, true);
                }

                if (tenantId != CurrentTenant.Id)
                {
                    tenantChanged = true;
                }
            }

            using var tenantChange = CurrentTenant.Change(tenantId);

            if (tenantChanged)
            {
                official = await _weChatAppRepository.GetOfficialAppByAppIdAsync(input.AppId);
            }

            var loginProvider = await _officialLoginProviderProvider.GetAppLoginProviderAsync(official);
            var providerKey = openId;

            return new LoginResultInfoModel
            {
                Official = official,
                LoginProvider = loginProvider,
                ProviderKey = providerKey,
                Code2AccessTokenResponse = code2AccessTokenResponse
            };
        }

        protected virtual async Task<WeChatAppUser> UpdateWeChatAppUserAsync(IdentityUser identityUser, WeChatApp official, string openId)
        {
            var mpUserMapping = await _weChatAppUserRepository.FindAsync(x =>
                x.WeChatAppId == official.Id && x.UserId == identityUser.Id);

            if (mpUserMapping == null)
            {
                mpUserMapping = new WeChatAppUser(GuidGenerator.Create(), CurrentTenant.Id, official.Id,
                    identityUser.Id, null, openId);

                return await _weChatAppUserRepository.InsertAsync(mpUserMapping, true);
            }

            mpUserMapping.SetOpenId(openId);

            return await _weChatAppUserRepository.UpdateAsync(mpUserMapping, true);
        }

        protected virtual async Task TryCreateUserInfoAsync(IdentityUser identityUser, UserInfoModel userInfoModel)
        {
            var userInfo = await _userInfoRepository.FindAsync(x => x.UserId == identityUser.Id);

            if (userInfo == null)
            {
                userInfo = new UserInfo(GuidGenerator.Create(), CurrentTenant.Id, identityUser.Id, userInfoModel);

                await _userInfoRepository.InsertAsync(userInfo, true);
            }
            else
            {
                // 注意：2021年4月13日后，登录时获得的UserInfo将是匿名信息，非真实用户信息，因此不再覆盖更新
                // https://github.com/EasyAbp/WeChatManagement/issues/20
                // https://developers.weixin.qq.com/community/develop/doc/000cacfa20ce88df04cb468bc52801
            }
        }
    }
}
