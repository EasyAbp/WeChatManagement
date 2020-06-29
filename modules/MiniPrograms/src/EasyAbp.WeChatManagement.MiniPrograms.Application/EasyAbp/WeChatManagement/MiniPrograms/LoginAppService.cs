using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Dtos;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.IdentityModel;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class LoginAppService : MiniProgramsAppService, ILoginAppService
    {
        private readonly LoginService _loginService;
        private readonly SignatureChecker _signatureChecker;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IMiniProgramUserRepository _miniProgramUserRepository;
        private readonly IMiniProgramLoginNewUserCreator _miniProgramLoginNewUserCreator;
        private readonly IMiniProgramLoginProviderProvider _miniProgramLoginProviderProvider;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IMiniProgramRepository _miniProgramRepository;

        public LoginAppService(
            LoginService loginService,
            SignatureChecker signatureChecker,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IUserInfoRepository userInfoRepository,
            IMiniProgramUserRepository miniProgramUserRepository,
            IMiniProgramLoginNewUserCreator miniProgramLoginNewUserCreator,
            IMiniProgramLoginProviderProvider miniProgramLoginProviderProvider,
            IdentityUserManager identityUserManager,
            IMiniProgramRepository miniProgramRepository)
        {
            _loginService = loginService;
            _signatureChecker = signatureChecker;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _userInfoRepository = userInfoRepository;
            _miniProgramUserRepository = miniProgramUserRepository;
            _miniProgramLoginNewUserCreator = miniProgramLoginNewUserCreator;
            _miniProgramLoginProviderProvider = miniProgramLoginProviderProvider;
            _identityUserManager = identityUserManager;
            _miniProgramRepository = miniProgramRepository;
        }
        
        public virtual async Task<TokenResponse> RequestTokensAsync(RequestTokensDto input)
        {
            var miniProgram = await _miniProgramRepository.GetAsync(x => x.AppId == input.AppId);
            
            var code2SessionResponse =
                await _loginService.Code2SessionAsync(miniProgram.AppId, miniProgram.AppSecret, input.Code);

            await _signatureChecker.Check(input.RawData, code2SessionResponse.SessionKey, input.Signature);

            var openId = code2SessionResponse.OpenId;
            var unionId = code2SessionResponse.UnionId;

            string loginProvider;
            string providerKey;

            // 如果 auth.code2Session 没有返回用户的 UnionId
            if (unionId.IsNullOrWhiteSpace())
            {
                if (!input.EncryptedData.IsNullOrWhiteSpace() && !input.Iv.IsNullOrWhiteSpace())
                {
                    // 方法1：通过 EncryptedData 和 Iv 解密获得用户的 UnionId
                    // Todo: 调用 EasyAbp.Abp.WeChat 的解密方法
                }
                else if (miniProgram.OpenAppId.IsNullOrWhiteSpace())
                {
                    // 方法2：尝试通过 OpenId 在 MiniProgramUser 实体中查找用户的 UnionId
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
            
            await UpdateMiniProgramUserAsync(identityUser, miniProgram, code2SessionResponse);
            await UpdateUserInfoAsync(identityUser, input.UserInfo);
            
            return await RequestTokenByOpenIdAsync(input.AppId, unionId, openId);
        }

        public virtual Task<ListResultDto<BasicTenantInfo>> GetTenantsAsync(string appId, string code)
        {
            throw new System.NotImplementedException();
        }
        
        protected virtual async Task UpdateMiniProgramUserAsync(IdentityUser identityUser, MiniProgram miniProgram, Code2SessionResponse response)
        {
            var mpUserMapping = await _miniProgramUserRepository.FindAsync(x =>
                x.MiniProgramId == miniProgram.Id && x.UserId == identityUser.Id);

            if (mpUserMapping == null)
            {
                mpUserMapping = new MiniProgramUser(GuidGenerator.Create(), CurrentTenant.Id, miniProgram.Id,
                    identityUser.Id, response.UnionId, response.OpenId);

                await _miniProgramUserRepository.InsertAsync(mpUserMapping, true);
            }
            else
            {
                mpUserMapping.SetOpenId(response.OpenId);
                mpUserMapping.SetUnionId(response.UnionId);
                
                // Todo: 更新 SessionKey

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
        
        protected virtual async Task<TokenResponse> RequestTokenByOpenIdAsync(string appId, string unionId, string openId)
        {
            var client = _httpClientFactory.CreateClient(MiniProgramConsts.IdentityServerHttpClientName);

            return await client.RequestTokenAsync(new TokenRequest
            {
                Address = _configuration["AuthServer:Authority"] + "/connect/token",
                GrantType = MiniProgramConsts.GrantType,

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
    }
}