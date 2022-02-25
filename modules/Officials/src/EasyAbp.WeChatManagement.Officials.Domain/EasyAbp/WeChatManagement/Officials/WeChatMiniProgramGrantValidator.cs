using EasyAbp.WeChatManagement.Common.WeChatApps;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Security.Claims;

namespace EasyAbp.WeChatManagement.Officials
{
    public class WeChatOfficialGrantValidator : IExtensionGrantValidator, ITransientDependency
    {
        public string GrantType => WeChatOfficialConsts.GrantType;

        private readonly IOfficialLoginProviderProvider _officialLoginProviderProvider;
        private readonly IWeChatAppRepository _weChatAppRepository;
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IdentityUserManager _identityUserManager;

        public WeChatOfficialGrantValidator(
            IOfficialLoginProviderProvider officialLoginProviderProvider,
            IWeChatAppRepository weChatAppRepository,
            IOptions<IdentityOptions> identityOptions,
            IdentityUserManager identityUserManager)
        {
            _officialLoginProviderProvider = officialLoginProviderProvider;
            _weChatAppRepository = weChatAppRepository;
            _identityOptions = identityOptions;
            _identityUserManager = identityUserManager;
        }

        public virtual async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            await _identityOptions.SetAsync();

            var appId = context.Request.Raw.Get("appid");
            var openId = context.Request.Raw.Get("openid");

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

            var official = await _weChatAppRepository.GetOfficialAppByAppIdAsync(appId);

            var loginProvider = await _officialLoginProviderProvider.GetAppLoginProviderAsync(official);
            var providerKey = openId;

            var identityUser = await _identityUserManager.FindByLoginAsync(loginProvider, providerKey);

            var claims = new List<Claim>
            {
                // 记录 appid
                new Claim("appid", appId)
            };

            if (identityUser.TenantId.HasValue)
            {
                claims.Add(new Claim(AbpClaimTypes.TenantId, identityUser.TenantId?.ToString()));
            }

            claims.AddRange(identityUser.Claims.Select(item => new Claim(item.ClaimType, item.ClaimValue)));

            context.Result = new GrantValidationResult(identityUser.Id.ToString(), GrantType, claims);
        }
    }
}
