using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure;
using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;
using EasyAbp.WeChatManagement.MiniPrograms.Identity.Dtos;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Identity;
using Volo.Abp.Json;
using Volo.Abp.Users;

namespace EasyAbp.WeChatManagement.MiniPrograms.Identity
{
    [Authorize]
    public class ProfileAppService : MiniProgramsAppService, IProfileAppService
    {
        private readonly LoginService _loginService;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IMiniProgramRepository _miniProgramRepository;

        public ProfileAppService(
            LoginService loginService,
            IdentityUserManager identityUserManager,
            IJsonSerializer jsonSerializer,
            IMiniProgramRepository miniProgramRepository)
        {
            _loginService = loginService;
            _identityUserManager = identityUserManager;
            _jsonSerializer = jsonSerializer;
            _miniProgramRepository = miniProgramRepository;
        }
        
        /// <summary>
        /// 通过微信开放能力获取并给当前用户绑定手机号，更新信息：https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/getPhoneNumber.html
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        /// <exception cref="AbpIdentityResultException"></exception>
        public async Task BindPhoneNumberAsync(BindPhoneNumberInput input)
        {
            var user = await _identityUserManager.GetByIdAsync(CurrentUser.GetId());
            
            var miniProgram = await _miniProgramRepository.GetAsync(x => x.AppId == input.AppId);

            var response = await _loginService.Code2SessionAsync(miniProgram.AppId, miniProgram.AppSecret, input.Code);

            if (response.ErrorCode != 0)
            {
                throw new BusinessException(message: $"WeChat error: [{response.ErrorCode}]: {response.ErrorMessage}");
            }

            var decryptedData = _jsonSerializer.Deserialize<Dictionary<string, object>>(AesHelper
                .AesDecrypt(input.EncryptedData, input.Iv, response.SessionKey));

            var phoneNumber = decryptedData["phoneNumber"] as string;

            _identityUserManager.RegisterTokenProvider(TokenOptions.DefaultPhoneProvider,
                new StaticPhoneNumberTokenProvider());

            var token = await _identityUserManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);

            var identityResult = await _identityUserManager.ChangePhoneNumberAsync(user, phoneNumber, token);

            if (!identityResult.Succeeded)
            {
                throw new AbpIdentityResultException(identityResult);
            }
        }
    }
}