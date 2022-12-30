using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Official.Services.Login;
using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using EasyAbp.WeChatManagement.Officials.Login.Dtos;
using EasyAbp.WeChatManagement.Officials.Settings;
using EasyAbp.WeChatManagement.Officials.UserInfos;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace EasyAbp.WeChatManagement.Officials.Login
{
    public class LoginAppService : OfficialsAppService, ILoginAppService
    {
        protected virtual string BindPolicyName { get; set; }
        protected virtual string UnbindPolicyName { get; set; }

        private readonly AbpSignInManager _signInManager;
        private readonly IDataFilter _dataFilter;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IWeChatAppRepository _weChatAppRepository;
        private readonly IStringEncryptionService _stringEncryptionService;
        private readonly IWeChatAppUserRepository _weChatAppUserRepository;
        private readonly IAbpWeChatServiceFactory _abpWeChatServiceFactory;
        private readonly IOfficialLoginNewUserCreator _OfficialLoginNewUserCreator;
        private readonly IOfficialLoginProviderProvider _OfficialLoginProviderProvider;
        private readonly IDistributedCache<OfficialPcLoginAuthorizationCacheItem> _pcLoginAuthorizationCache;

        public LoginAppService(
            AbpSignInManager signInManager,
            IDataFilter dataFilter,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IUserInfoRepository userInfoRepository,
            IWeChatAppRepository weChatAppRepository,
            IStringEncryptionService stringEncryptionService,
            IWeChatAppUserRepository weChatAppUserRepository,
            IAbpWeChatServiceFactory abpWeChatServiceFactory,
            IOfficialLoginNewUserCreator OfficialLoginNewUserCreator,
            IOfficialLoginProviderProvider OfficialLoginProviderProvider,
            IDistributedCache<OfficialPcLoginAuthorizationCacheItem> pcLoginAuthorizationCache)
        {
            _signInManager = signInManager;
            _dataFilter = dataFilter;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _userInfoRepository = userInfoRepository;
            _weChatAppRepository = weChatAppRepository;
            _stringEncryptionService = stringEncryptionService;
            _weChatAppUserRepository = weChatAppUserRepository;
            _abpWeChatServiceFactory = abpWeChatServiceFactory;
            _OfficialLoginNewUserCreator = OfficialLoginNewUserCreator;
            _OfficialLoginProviderProvider = OfficialLoginProviderProvider;
            _pcLoginAuthorizationCache = pcLoginAuthorizationCache;
        }

        [Authorize]
        public virtual async Task BindAsync(LoginInput input)
        {
            await CheckBindPolicyAsync();

            var loginResult = await GetLoginResultAsync(input);

            using var tenantChange = CurrentTenant.Change(loginResult.Official.TenantId);
        }

        [Authorize]
        public virtual async Task UnbindAsync(LoginInput input)
        {
            await CheckUnbindPolicyAsync();

            var loginResult = await GetLoginResultAsync(input);

            using var tenantChange = CurrentTenant.Change(loginResult.Official.TenantId);
        }

        [UnitOfWork(IsDisabled = true)]
        public virtual async Task<LoginOutput> LoginAsync(LoginInput input)
        {
            var loginResult = await GetLoginResultAsync(input);

            using var tenantChange = CurrentTenant.Change(loginResult.Official.TenantId);


            using (var uow = UnitOfWorkManager.Begin(new AbpUnitOfWorkOptions(true), true))
            {
                var identityUser =
                    await _OfficialLoginNewUserCreator.CreateAsync(loginResult.LoginProvider,
                        loginResult.ProviderKey);

                await UpdateWeChatAppUserAsync(identityUser, loginResult.Official, loginResult.UnionId,
                    loginResult.Code2AccessTokenResponse.OpenId, null);

                await TryCreateUserInfoAsync(identityUser, await GenerateFakeUserInfoAsync());

                await uow.CompleteAsync();
            }

            var response = await RequestAuthServerLoginAsync(input.AppId, loginResult.UnionId,
                loginResult.Code2AccessTokenResponse.OpenId, input.Scope);

            if (response.IsError)
            {
                throw response.Exception ?? new AbpException(response.Raw);
            }

            return new LoginOutput
            {
                TenantId = loginResult.Official.TenantId,
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

            WeChatApp Official;

            if (input.LookupUseRecentlyTenant)
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    Official = await _weChatAppRepository.FirstOrDefaultAsync(x =>
                        x.AppId == input.AppId && x.Type == WeChatAppType.Official);
                }
            }
            else
            {
                Official = await _weChatAppRepository.GetOfficialAppByAppIdAsync(input.AppId);
            }

            LoginWeService loginWeService = null;

            using (CurrentTenant.Change(Official.TenantId))
            {
                loginWeService = await _abpWeChatServiceFactory.CreateAsync<LoginWeService>(Official.AppId);
            }

            var code2AccessToken =
                await loginWeService.Code2AccessTokenAsync(Official.AppId,
                    _stringEncryptionService.Decrypt(Official.EncryptedAppSecret), input.Code);

            // wechat code2session 错误
            if (code2AccessToken.ErrorCode != 0)
            {
                throw new WeChatBusinessException(code2AccessToken.ErrorCode, code2AccessToken.ErrorMessage);
            }

            var openId = code2AccessToken.OpenId;
            // var unionId = code2AccessToken.UnionId; => Wechat项目目前没有unionId返回，实际微信接口有返回
            string unionId = null;

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
                Official = await _weChatAppRepository.GetOfficialAppByAppIdAsync(input.AppId);
            }

            string loginProvider;
            string providerKey;

            if (!unionId.IsNullOrWhiteSpace())
            {
                loginProvider = await _OfficialLoginProviderProvider.GetOpenLoginProviderAsync(Official);
                providerKey = unionId;
            }
            else
            {
                loginProvider = await _OfficialLoginProviderProvider.GetAppLoginProviderAsync(Official);
                providerKey = openId;
            }

            return new LoginResultInfoModel
            {
                Official = Official,
                LoginProvider = loginProvider,
                ProviderKey = providerKey,
                UnionId = unionId,
                Code2AccessTokenResponse = code2AccessToken
            };
        }

        public virtual async Task<string> RefreshAsync(RefreshInput input)
        {
            return (await RequestAuthServerRefreshAsync(input.RefreshToken))?.Raw;
        }

        protected virtual async Task UpdateWeChatAppUserAsync(IdentityUser identityUser, WeChatApp Official,
            string unionId, string openId, string sessionKey)
        {
            var mpUserMapping = await _weChatAppUserRepository.FindAsync(x =>
                x.WeChatAppId == Official.Id && x.UserId == identityUser.Id);

            if (mpUserMapping == null)
            {
                mpUserMapping = new WeChatAppUser(GuidGenerator.Create(), CurrentTenant.Id, Official.Id,
                    identityUser.Id, unionId, openId);

                await _weChatAppUserRepository.InsertAsync(mpUserMapping, true);
            }
            else
            {
                mpUserMapping.SetOpenId(openId);
                mpUserMapping.SetUnionId(unionId);

                mpUserMapping.UpdateSessionKey(sessionKey, _stringEncryptionService, Clock);

                await _weChatAppUserRepository.UpdateAsync(mpUserMapping, true);
            }
        }

        protected virtual async Task RemoveWeChatAppUserAsync(IdentityUser identityUser, WeChatApp Official)
        {
            var mpUserMapping = await _weChatAppUserRepository.GetAsync(x =>
                x.WeChatAppId == Official.Id && x.UserId == identityUser.Id);

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

        protected virtual async Task<TokenResponse> RequestAuthServerLoginAsync(string appId, string unionId,
            string openId, string scope)
        {
            var client = _httpClientFactory.CreateClient(WeChatOfficialConsts.AuthServerHttpClientName);

            var request = new TokenRequest
            {
                Address = _configuration["WeChatManagement:Officials:AuthServer:Authority"] + "/connect/token",
                GrantType = WeChatOfficialConsts.GrantType,

                ClientId = _configuration["WeChatManagement:Officials:AuthServer:ClientId"],
                ClientSecret = _configuration["WeChatManagement:Officials:AuthServer:ClientSecret"],

                Parameters =
                {
                    { "appid", appId },
                    { "unionid", unionId },
                    { "openid", openId },
                    { "scope", scope },
                }
            };

            request.Headers.Add(GetTenantHeaderName(), CurrentTenant.Id?.ToString());

            return await client.RequestTokenAsync(request);
        }

        protected virtual async Task<TokenResponse> RequestAuthServerRefreshAsync(string refreshToken)
        {
            var client = _httpClientFactory.CreateClient(WeChatOfficialConsts.AuthServerHttpClientName);

            var request = new RefreshTokenRequest
            {
                Address = _configuration["WeChatManagement:Officials:AuthServer:Authority"] + "/connect/token",

                ClientId = _configuration["WeChatManagement:Officials:AuthServer:ClientId"],
                ClientSecret = _configuration["WeChatManagement:Officials:AuthServer:ClientSecret"],

                RefreshToken = refreshToken
            };

            request.Headers.Add(GetTenantHeaderName(), CurrentTenant.Id?.ToString());

            return await client.RequestRefreshTokenAsync(request);
        }

        protected virtual string GetTenantHeaderName()
        {
            return "__tenant";
        }

}
}