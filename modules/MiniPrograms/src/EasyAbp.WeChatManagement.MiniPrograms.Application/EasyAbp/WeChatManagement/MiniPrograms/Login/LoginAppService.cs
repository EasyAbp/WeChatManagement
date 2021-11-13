using EasyAbp.Abp.WeChat;
using EasyAbp.Abp.WeChat.MiniProgram;
using EasyAbp.Abp.WeChat.MiniProgram.Services.ACode;
using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;
using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using EasyAbp.WeChatManagement.MiniPrograms.Settings;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve.Contributors;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class LoginAppService : MiniProgramsAppService, ILoginAppService
    {
        protected virtual string BindPolicyName { get; set; }
        protected virtual string UnbindPolicyName { get; set; }

        private readonly LoginService _loginService;
        private readonly ACodeService _aCodeService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IDataFilter _dataFilter;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IWeChatMiniProgramAsyncLocal _weChatMiniProgramAsyncLocal;
        private readonly IWeChatAppRepository _weChatAppRepository;
        private readonly IWeChatAppUserRepository _weChatAppUserRepository;
        private readonly IMiniProgramLoginNewUserCreator _miniProgramLoginNewUserCreator;
        private readonly IMiniProgramLoginProviderProvider _miniProgramLoginProviderProvider;
        private readonly IDistributedCache<MiniProgramPcLoginAuthorizationCacheItem> _pcLoginAuthorizationCache;
        private readonly IDistributedCache<MiniProgramPcLoginUserLimitCacheItem> _pcLoginUserLimitCache;
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IdentityUserManager _identityUserManager;

        public LoginAppService(
            LoginService loginService,
            ACodeService aCodeService,
            SignInManager<IdentityUser> signInManager,
            IDataFilter dataFilter,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IUserInfoRepository userInfoRepository,
            IWeChatMiniProgramAsyncLocal weChatMiniProgramAsyncLocal,
            IWeChatAppRepository weChatAppRepository,
            IWeChatAppUserRepository weChatAppUserRepository,
            IMiniProgramLoginNewUserCreator miniProgramLoginNewUserCreator,
            IMiniProgramLoginProviderProvider miniProgramLoginProviderProvider,
            IDistributedCache<MiniProgramPcLoginAuthorizationCacheItem> pcLoginAuthorizationCache,
            IDistributedCache<MiniProgramPcLoginUserLimitCacheItem> pcLoginUserLimitCache,
            IOptions<IdentityOptions> identityOptions,
            IdentityUserManager identityUserManager)
        {
            _loginService = loginService;
            _aCodeService = aCodeService;
            _signInManager = signInManager;
            _dataFilter = dataFilter;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _userInfoRepository = userInfoRepository;
            _weChatMiniProgramAsyncLocal = weChatMiniProgramAsyncLocal;
            _weChatAppRepository = weChatAppRepository;
            _weChatAppUserRepository = weChatAppUserRepository;
            _miniProgramLoginNewUserCreator = miniProgramLoginNewUserCreator;
            _miniProgramLoginProviderProvider = miniProgramLoginProviderProvider;
            _pcLoginAuthorizationCache = pcLoginAuthorizationCache;
            _pcLoginUserLimitCache = pcLoginUserLimitCache;
            _identityOptions = identityOptions;
            _identityUserManager = identityUserManager;
        }

        [Authorize]
        public virtual async Task BindAsync(LoginInput input)
        {
            await CheckBindPolicyAsync();

            var loginResult = await GetLoginResultAsync(input);

            using var tenantChange = CurrentTenant.Change(loginResult.MiniProgram.TenantId);

            await _identityOptions.SetAsync();

            if (await _identityUserManager.FindByLoginAsync(loginResult.LoginProvider, loginResult.ProviderKey) != null)
            {
                throw new WeChatAccountHasBeenBoundException();
            }

            var identityUser = await _identityUserManager.GetByIdAsync(CurrentUser.GetId());

            (await _identityUserManager.AddLoginAsync(identityUser,
                new UserLoginInfo(loginResult.LoginProvider, loginResult.ProviderKey,
                    WeChatManagementCommonConsts.WeChatUserLoginInfoDisplayName))).CheckErrors();

            await UpdateWeChatAppUserAsync(identityUser, loginResult.MiniProgram, loginResult.UnionId,
                loginResult.Code2SessionResponse.OpenId, loginResult.Code2SessionResponse.SessionKey);

            await TryCreateUserInfoAsync(identityUser, await GenerateFakeUserInfoAsync());
        }

        [Authorize]
        public virtual async Task UnbindAsync(LoginInput input)
        {
            await CheckUnbindPolicyAsync();

            var loginResult = await GetLoginResultAsync(input);

            using var tenantChange = CurrentTenant.Change(loginResult.MiniProgram.TenantId);

            await _identityOptions.SetAsync();

            if (await _identityUserManager.FindByLoginAsync(loginResult.LoginProvider, loginResult.ProviderKey) == null)
            {
                throw new WeChatAccountHasNotBeenBoundException();
            }

            var identityUser = await _identityUserManager.GetByIdAsync(CurrentUser.GetId());

            (await _identityUserManager.RemoveLoginAsync(identityUser, loginResult.LoginProvider, loginResult.ProviderKey)).CheckErrors();

            await RemoveWeChatAppUserAsync(identityUser, loginResult.MiniProgram);

            if (!await _weChatAppUserRepository.AnyInWeChatAppTypeAsync(WeChatAppType.MiniProgram,
                x => x.UserId == identityUser.Id))
            {
                await TryRemoveUserInfoAsync(identityUser);
            }
        }

        public virtual async Task<LoginOutput> LoginAsync(LoginInput input)
        {
            var loginResult = await GetLoginResultAsync(input);

            using var tenantChange = CurrentTenant.Change(loginResult.MiniProgram.TenantId);

            await _identityOptions.SetAsync();

            using (var uow = UnitOfWorkManager.Begin(new AbpUnitOfWorkOptions(true), true))
            {
                var identityUser =
                    await _identityUserManager.FindByLoginAsync(loginResult.LoginProvider, loginResult.ProviderKey) ??
                    await _miniProgramLoginNewUserCreator.CreateAsync(loginResult.LoginProvider,
                        loginResult.ProviderKey);

                await UpdateWeChatAppUserAsync(identityUser, loginResult.MiniProgram, loginResult.UnionId,
                    loginResult.Code2SessionResponse.OpenId, loginResult.Code2SessionResponse.SessionKey);

                await TryCreateUserInfoAsync(identityUser, await GenerateFakeUserInfoAsync());

                await uow.CompleteAsync();
            }

            var response = await RequestIds4LoginAsync(input.AppId, loginResult.UnionId,
                loginResult.Code2SessionResponse.OpenId);

            if (response.IsError)
            {
                throw response.Exception;
            }
            
            return new LoginOutput
            {
                TenantId = loginResult.MiniProgram.TenantId,
                RawData = response.Raw
            };
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

        protected virtual async Task CheckBindPolicyAsync()
        {
            await CheckPolicyAsync(BindPolicyName);
        }

        protected virtual async Task CheckUnbindPolicyAsync()
        {
            await CheckPolicyAsync(UnbindPolicyName);
        }

        protected virtual async Task<LoginResultInfoModel> GetLoginResultAsync(LoginInput input)
        {
            var tenantId = CurrentTenant.Id;
            var tenantChanged = false;

            WeChatApp miniProgram;

            if (input.LookupUseRecentlyTenant)
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    miniProgram = await _weChatAppRepository.FirstOrDefaultAsync(x =>
                        x.AppId == input.AppId && x.Type == WeChatAppType.MiniProgram);
                }
            }
            else
            {
                miniProgram = await _weChatAppRepository.GetMiniProgramAppByAppIdAsync(input.AppId);
            }

            var code2SessionResponse =
                await _loginService.Code2SessionAsync(miniProgram.AppId, miniProgram.AppSecret, input.Code);

            var openId = code2SessionResponse.OpenId;
            var unionId = code2SessionResponse.UnionId;

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
                miniProgram = await _weChatAppRepository.GetMiniProgramAppByAppIdAsync(input.AppId);
            }

            string loginProvider;
            string providerKey;

            if (!unionId.IsNullOrWhiteSpace())
            {
                loginProvider = await _miniProgramLoginProviderProvider.GetOpenLoginProviderAsync(miniProgram);
                providerKey = unionId;
            }
            else
            {
                loginProvider = await _miniProgramLoginProviderProvider.GetAppLoginProviderAsync(miniProgram);
                providerKey = openId;
            }
            return new LoginResultInfoModel
            {
                MiniProgram = miniProgram,
                LoginProvider = loginProvider,
                ProviderKey = providerKey,
                UnionId = unionId,
                Code2SessionResponse = code2SessionResponse
            };
        }

        public virtual async Task<string> RefreshAsync(RefreshInput input)
        {
            return (await RequestIds4RefreshAsync(input.RefreshToken))?.Raw;
        }

        protected virtual async Task UpdateWeChatAppUserAsync(IdentityUser identityUser, WeChatApp miniProgram, string unionId, string openId, string sessionKey)
        {
            var mpUserMapping = await _weChatAppUserRepository.FindAsync(x =>
                x.WeChatAppId == miniProgram.Id && x.UserId == identityUser.Id);

            if (mpUserMapping == null)
            {
                mpUserMapping = new WeChatAppUser(GuidGenerator.Create(), CurrentTenant.Id, miniProgram.Id,
                    identityUser.Id, unionId, openId);

                await _weChatAppUserRepository.InsertAsync(mpUserMapping, true);
            }
            else
            {
                mpUserMapping.SetOpenId(openId);
                mpUserMapping.SetUnionId(unionId);

                mpUserMapping.UpdateSessionKey(sessionKey, Clock);

                await _weChatAppUserRepository.UpdateAsync(mpUserMapping, true);
            }
        }

        protected virtual async Task RemoveWeChatAppUserAsync(IdentityUser identityUser, WeChatApp miniProgram)
        {
            var mpUserMapping = await _weChatAppUserRepository.GetAsync(x =>
                x.WeChatAppId == miniProgram.Id && x.UserId == identityUser.Id);

            await _weChatAppUserRepository.DeleteAsync(mpUserMapping, true);
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

        protected virtual async Task TryRemoveUserInfoAsync(IdentityUser identityUser)
        {
            var userInfo = await _userInfoRepository.FindAsync(x => x.UserId == identityUser.Id);

            if (userInfo != null)
            {
                await _userInfoRepository.DeleteAsync(userInfo, true);
            }
        }

        protected virtual async Task<TokenResponse> RequestIds4LoginAsync(string appId, string unionId, string openId)
        {
            var client = _httpClientFactory.CreateClient(WeChatMiniProgramConsts.IdentityServerHttpClientName);

            var request = new TokenRequest
            {
                Address = _configuration["AuthServer:Authority"] + "/connect/token",
                GrantType = WeChatMiniProgramConsts.GrantType,

                ClientId = _configuration["AuthServer:ClientId"],
                ClientSecret = _configuration["AuthServer:ClientSecret"],

                Parameters =
                {
                    {"appid", appId},
                    {"unionid", unionId},
                    {"openid", openId},
                }
            };

            request.Headers.Add(GetTenantHeaderName(), CurrentTenant.Id?.ToString());

            return await client.RequestTokenAsync(request);
        }

        protected virtual async Task<TokenResponse> RequestIds4RefreshAsync(string refreshToken)
        {
            var client = _httpClientFactory.CreateClient(WeChatMiniProgramConsts.IdentityServerHttpClientName);

            var request = new RefreshTokenRequest
            {
                Address = _configuration["AuthServer:Authority"] + "/connect/token",

                ClientId = _configuration["AuthServer:ClientId"],
                ClientSecret = _configuration["AuthServer:ClientSecret"],

                RefreshToken = refreshToken
            };

            request.Headers.Add(GetTenantHeaderName(), CurrentTenant.Id?.ToString());

            return await client.RequestRefreshTokenAsync(request);
        }

        protected virtual string GetTenantHeaderName()
        {
            return "__tenant";
        }

        public virtual async Task<GetPcLoginACodeOutput> GetPcLoginACodeAsync(string miniProgramName,
            string handlePage = null)
        {
            var miniProgram = await _weChatAppRepository.GetMiniProgramAppByNameAsync(miniProgramName);

            var options = new AbpWeChatMiniProgramOptions
            {
                OpenAppId = miniProgram.OpenAppIdOrName,
                AppId = miniProgram.AppId,
                AppSecret = miniProgram.AppSecret,
                EncodingAesKey = miniProgram.EncodingAesKey,
                Token = miniProgram.Token
            };

            using (_weChatMiniProgramAsyncLocal.Change(options))
            {
                var token = Guid.NewGuid().ToString("N");

                if (handlePage.IsNullOrWhiteSpace())
                {
                    handlePage = await SettingProvider.GetOrNullAsync(MiniProgramsSettings.PcLogin.HandlePage);
                }

                var aCodeResponse = await _aCodeService.GetUnlimitedACodeAsync(token, handlePage);

                if (aCodeResponse.ErrorCode != 0)
                {
                    throw new WeChatBusinessException(aCodeResponse.ErrorCode, aCodeResponse.ErrorMessage);
                }

                return new GetPcLoginACodeOutput
                {
                    HandlePage = handlePage,
                    Token = token,
                    ACode = aCodeResponse.BinaryData
                };
            }
        }

        [Authorize]
        public virtual async Task AuthorizePcAsync(AuthorizePcInput input)
        {
            if (await _pcLoginUserLimitCache.GetAsync(CurrentUser.GetId().ToString()) != null)
            {
                throw new PcLoginAuthorizeTooFrequentlyException();
            }
            
            var miniProgram = await _weChatAppRepository.GetMiniProgramAppByAppIdAsync(input.AppId);

            var weChatAppUser = await _weChatAppUserRepository.GetAsync(x =>
                x.WeChatAppId == miniProgram.Id && x.UserId == CurrentUser.GetId());

            await _pcLoginAuthorizationCache.SetAsync(input.Token,
                new MiniProgramPcLoginAuthorizationCacheItem
                {
                    AppId = input.AppId,
                    UnionId = weChatAppUser.UnionId,
                    OpenId = weChatAppUser.OpenId,
                    UserId = CurrentUser.GetId()
                },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            await _pcLoginUserLimitCache.SetAsync(CurrentUser.GetId().ToString(),
                new MiniProgramPcLoginUserLimitCacheItem(),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3)
                });
        }

        public virtual async Task<PcLoginOutput> PcLoginAsync(PcLoginInput input)
        {
            await _identityOptions.SetAsync();

            var cacheItem = await _pcLoginAuthorizationCache.GetAsync(input.Token);

            if (cacheItem == null)
            {
                return new PcLoginOutput { IsSuccess = false };
            }

            await _pcLoginAuthorizationCache.RemoveAsync(input.Token);

            var user = await _identityUserManager.GetByIdAsync(cacheItem.UserId);

            await _signInManager.SignInAsync(user, false);

            return new PcLoginOutput { IsSuccess = true };
        }
        
        public virtual async Task<PcLoginRequestTokensOutput> PcLoginRequestTokensAsync(PcLoginInput input)
        {
            await _identityOptions.SetAsync();

            var cacheItem = await _pcLoginAuthorizationCache.GetAsync(input.Token);

            if (cacheItem == null)
            {
                return new PcLoginRequestTokensOutput { IsSuccess = false };
            }
            
            await _pcLoginAuthorizationCache.RemoveAsync(input.Token);

            var response = await RequestIds4LoginAsync(cacheItem.AppId, cacheItem.UnionId,
                cacheItem.OpenId);

            if (response.IsError)
            {
                throw response.Exception;
            }
            
            return new PcLoginRequestTokensOutput
            {
                IsSuccess = true,
                RawData = response.Raw
            };
        }
    }
}