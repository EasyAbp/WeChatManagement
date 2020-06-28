using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Security.Claims;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class WeChatMiniProgramGrantValidator : IExtensionGrantValidator, ITransientDependency
    {
        public string GrantType => "WeChatMiniProgram_credentials";
        
        private const string WeChatAppLoginProviderPrefix = "WeChatApp_";
        private const string WeChatOpenLoginProviderPrefix = "WeChatOpen_";

        private readonly IMiniProgramLoginNewUserCreator _miniProgramLoginNewUserCreator;
        private readonly IMiniProgramRepository _miniProgramRepository;
        private readonly IdentityUserManager _identityUserManager;

        public WeChatMiniProgramGrantValidator(
            IMiniProgramLoginNewUserCreator miniProgramLoginNewUserCreator,
            IMiniProgramRepository miniProgramRepository,
            IdentityUserManager identityUserManager)
        {
            _miniProgramLoginNewUserCreator = miniProgramLoginNewUserCreator;
            _miniProgramRepository = miniProgramRepository;
            _identityUserManager = identityUserManager;
        }
        
        public virtual async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var appId = context.Request.Raw.Get("appid");
            var openId = context.Request.Raw.Get("openid");
            var unionId = context.Request.Raw.Get("unionid");
            
            if (string.IsNullOrWhiteSpace(appId))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant)
                {
                    ErrorDescription = "请提供有效的 appid"
                };

                return;
            }
            
            if (string.IsNullOrWhiteSpace(openId))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant)
                {
                    ErrorDescription = "微信服务器没有提供有效的 openid"
                };

                return;
            }

            var miniProgram = await _miniProgramRepository.GetAsync(x => x.AppId == appId);

            string loginProvider;
            string providerKey;

            if (miniProgram.OpenAppId.IsNullOrWhiteSpace())
            {
                loginProvider = WeChatAppLoginProviderPrefix + miniProgram.AppId;
                providerKey = openId;
            }
            else
            {
                loginProvider = WeChatOpenLoginProviderPrefix + miniProgram.OpenAppId;
                providerKey = unionId;
            }

            var identityUser = await _identityUserManager.FindByLoginAsync(loginProvider, providerKey) ??
                               await _miniProgramLoginNewUserCreator.CreateAsync(context);

            var claims = new List<Claim>
            {
                // 记录 appid
                new Claim("appid", appId)
            };

            claims.AddRange(identityUser.Claims.Select(item => new Claim(item.ClaimType, item.ClaimValue)));

            context.Result = new GrantValidationResult(identityUser.Id.ToString(), GrantType, claims);
        }
    }
}