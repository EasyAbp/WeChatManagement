using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Security.Claims;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class WeChatMiniProgramGrantValidator : IExtensionGrantValidator, ITransientDependency
    {
        public string GrantType => WeChatMiniProgramConsts.GrantType;

        private readonly IMiniProgramLoginProviderProvider _miniProgramLoginProviderProvider;
        private readonly IWeChatAppRepository _weChatAppRepository;
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IdentityUserManager _identityUserManager;

        public WeChatMiniProgramGrantValidator(
            IMiniProgramLoginProviderProvider miniProgramLoginProviderProvider,
            IWeChatAppRepository weChatAppRepository,
            IOptions<IdentityOptions> identityOptions,
            IdentityUserManager identityUserManager)
        {
            _miniProgramLoginProviderProvider = miniProgramLoginProviderProvider;
            _weChatAppRepository = weChatAppRepository;
            _identityOptions = identityOptions;
            _identityUserManager = identityUserManager;
        }
        
        public virtual async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            await _identityOptions.SetAsync();

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

            var miniProgram = await _weChatAppRepository.GetMiniProgramAppByAppIdAsync(appId);

            string loginProvider;
            string providerKey;

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

            var identityUser = await _identityUserManager.FindByLoginAsync(loginProvider, providerKey);
            
            var claims = new List<Claim>
            {
                // 记录 appid
                new("appid", appId)
            };
            
            if (identityUser.TenantId.HasValue)
            {
                claims.Add(new Claim(AbpClaimTypes.TenantId, identityUser.TenantId?.ToString() ?? string.Empty));
            }

            claims.AddRange(identityUser.Claims.Select(item => new Claim(item.ClaimType, item.ClaimValue)));

            context.Result = new GrantValidationResult(identityUser.Id.ToString(), GrantType, claims);
        }
    }
}