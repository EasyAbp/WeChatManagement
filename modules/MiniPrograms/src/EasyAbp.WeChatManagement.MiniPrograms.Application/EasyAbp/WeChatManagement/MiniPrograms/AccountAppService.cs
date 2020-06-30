using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure;
using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;
using EasyAbp.WeChatManagement.MiniPrograms.Dtos;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.IdentityModel;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class AccountAppService : MiniProgramsAppService, ILoginAppService
    {
        private readonly LoginService _loginService;
        private readonly SignatureChecker _signatureChecker;
        private readonly IDataFilter _dataFilter;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IMiniProgramUserRepository _miniProgramUserRepository;
        private readonly IMiniProgramLoginNewUserCreator _miniProgramLoginNewUserCreator;
        private readonly IMiniProgramLoginProviderProvider _miniProgramLoginProviderProvider;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IMiniProgramRepository _miniProgramRepository;

        public AccountAppService(
            LoginService loginService,
            SignatureChecker signatureChecker,
            IDataFilter dataFilter,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IUserInfoRepository userInfoRepository,
            IJsonSerializer jsonSerializer,
            IMiniProgramUserRepository miniProgramUserRepository,
            IMiniProgramLoginNewUserCreator miniProgramLoginNewUserCreator,
            IMiniProgramLoginProviderProvider miniProgramLoginProviderProvider,
            IdentityUserManager identityUserManager,
            IMiniProgramRepository miniProgramRepository)
        {
            _loginService = loginService;
            _signatureChecker = signatureChecker;
            _dataFilter = dataFilter;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _userInfoRepository = userInfoRepository;
            _jsonSerializer = jsonSerializer;
            _miniProgramUserRepository = miniProgramUserRepository;
            _miniProgramLoginNewUserCreator = miniProgramLoginNewUserCreator;
            _miniProgramLoginProviderProvider = miniProgramLoginProviderProvider;
            _identityUserManager = identityUserManager;
            _miniProgramRepository = miniProgramRepository;
        }
        
        public virtual async Task<TokenResponse> LoginAsync(LoginDto input)
        {
            var miniProgram = await _miniProgramRepository.GetAsync(x => x.AppId == input.AppId);
            
            var code2SessionResponse =
                await _loginService.Code2SessionAsync(miniProgram.AppId, miniProgram.AppSecret, input.Code);

            _signatureChecker.Check(input.RawData, code2SessionResponse.SessionKey, input.Signature);

            var openId = code2SessionResponse.OpenId;
            var unionId = code2SessionResponse.UnionId;
            
            if (input.LookupUseRecentlyTenant)
            {
                Guid? tenantId;
                
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    tenantId = await _miniProgramUserRepository.FindRecentlyTenantIdAsync(miniProgram.Id, openId);
                }

                using var tenantChange = CurrentTenant.Change(tenantId);
            }

            string loginProvider;
            string providerKey;

            // 如果 auth.code2Session 没有返回用户的 UnionId
            if (unionId.IsNullOrWhiteSpace())
            {
                if (!input.EncryptedData.IsNullOrWhiteSpace() && !input.Iv.IsNullOrWhiteSpace())
                {
                    // 方法1：通过 EncryptedData 和 Iv 解密获得用户的 UnionId
                    var decryptedData =
                        _jsonSerializer.Deserialize<Dictionary<string, object>>(
                            AesHelper.AesDecrypt(input.EncryptedData, input.Iv, code2SessionResponse.SessionKey));

                    unionId = decryptedData.GetOrDefault("unionId") as string;
                }
                else
                {
                    // 方法2：尝试通过 OpenId 在 MiniProgramUser 实体中查找用户的 UnionId
                    // Todo: should use IMiniProgramUserStore
                    unionId = await _miniProgramUserRepository.FindUnionIdByOpenIdAsync(miniProgram.Id, openId);
                }
            }

            if (unionId.IsNullOrWhiteSpace())
            {
                loginProvider = await _miniProgramLoginProviderProvider.GetAppLoginProviderAsync(miniProgram);
                providerKey = openId;
            }
            else
            {
                loginProvider = await _miniProgramLoginProviderProvider.GetOpenLoginProviderAsync(miniProgram);
                providerKey = unionId;
            }

            var identityUser = await _identityUserManager.FindByLoginAsync(loginProvider, providerKey) ??
                               await _miniProgramLoginNewUserCreator.CreateAsync(input.UserInfo, loginProvider, providerKey);
            
            await UpdateMiniProgramUserAsync(identityUser, miniProgram, unionId, openId, code2SessionResponse.SessionKey);
            await UpdateUserInfoAsync(identityUser, input.UserInfo);
            
            return await RequestIds4LoginAsync(input.AppId, unionId, openId);
        }

        public virtual async Task<TokenResponse> RefreshAsync(RefreshDto input)
        {
            return await RequestIds4RefreshAsync(input.RefreshToken);
        }

        protected virtual async Task UpdateMiniProgramUserAsync(IdentityUser identityUser, MiniProgram miniProgram, string unionId, string openId, string sessionKey)
        {
            var mpUserMapping = await _miniProgramUserRepository.FindAsync(x =>
                x.MiniProgramId == miniProgram.Id && x.UserId == identityUser.Id);

            if (mpUserMapping == null)
            {
                mpUserMapping = new MiniProgramUser(GuidGenerator.Create(), CurrentTenant.Id, miniProgram.Id,
                    identityUser.Id, unionId, openId);

                await _miniProgramUserRepository.InsertAsync(mpUserMapping, true);
            }
            else
            {
                mpUserMapping.SetOpenId(openId);
                mpUserMapping.SetUnionId(unionId);
                
                mpUserMapping.UpdateSessionKey(sessionKey, Clock);

                await _miniProgramUserRepository.UpdateAsync(mpUserMapping, true);
            }
        }

        protected virtual async Task UpdateUserInfoAsync(IdentityUser identityUser, UserInfoModel userInfoModel)
        {
            var userInfo = await _userInfoRepository.FindAsync(x => x.UserId == identityUser.Id);

            if (userInfo == null)
            {
                userInfo = new UserInfo(GuidGenerator.Create(), CurrentTenant.Id, identityUser.Id, userInfoModel);

                await _userInfoRepository.InsertAsync(userInfo, true);
            }
            else
            {
                userInfo.UpdateInfo(userInfoModel);

                await _userInfoRepository.UpdateAsync(userInfo, true);
            }
        }
        
        protected virtual async Task<TokenResponse> RequestIds4LoginAsync(string appId, string unionId, string openId)
        {
            var client = _httpClientFactory.CreateClient(MiniProgramConsts.IdentityServerHttpClientName);

            return await client.RequestTokenAsync(new TokenRequest
            {
                Address = _configuration["AuthServer:Authority"] + "/connect/token",
                GrantType = MiniProgramConsts.CustomGrantType,

                ClientId = _configuration["AuthServer:ClientId"],
                ClientSecret = _configuration["AuthServer:ClientSecret"],

                Parameters =
                {
                    {"appid", appId},
                    {"unionid", unionId},
                    {"openid", openId},
                }
            });
        }

        protected virtual async Task<TokenResponse> RequestIds4RefreshAsync(string refreshToken)
        {
            var client = _httpClientFactory.CreateClient(MiniProgramConsts.IdentityServerHttpClientName);

            return await client.RequestTokenAsync(new RefreshTokenRequest
            {
                Address = _configuration["AuthServer:Authority"] + "/connect/token",
                GrantType = OidcConstants.GrantTypes.RefreshToken,

                ClientId = _configuration["AuthServer:ClientId"],
                ClientSecret = _configuration["AuthServer:ClientSecret"],
                
                RefreshToken = refreshToken
            });
        }
    }
}