using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.MiniProgram.Services.PhoneNumber;
using EasyAbp.WeChatManagement.MiniPrograms.Identity.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace EasyAbp.WeChatManagement.MiniPrograms.Identity
{
    [Authorize]
    public class ProfileAppService : MiniProgramsAppService, IProfileAppService
    {
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IAbpWeChatServiceFactory _abpWeChatServiceFactory;

        public ProfileAppService(
            IOptions<IdentityOptions> identityOptions,
            IdentityUserManager identityUserManager,
            IAbpWeChatServiceFactory abpWeChatServiceFactory)
        {
            ;
            _identityOptions = identityOptions;
            _identityUserManager = identityUserManager;
            _abpWeChatServiceFactory = abpWeChatServiceFactory;
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
            await _identityOptions.SetAsync();

            var user = await _identityUserManager.GetByIdAsync(CurrentUser.GetId());

            var phoneNumberWeService = await _abpWeChatServiceFactory.CreateAsync<PhoneNumberWeService>(input.AppId);

            var response = await phoneNumberWeService.GetPhoneNumberAsync(input.Code);

            if (response.ErrorCode != 0)
            {
                throw new BusinessException(message: $"WeChat error: [{response.ErrorCode}]: {response.ErrorMessage}");
            }

            var phoneNumber = response.PhoneInfo.PhoneNumber;

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