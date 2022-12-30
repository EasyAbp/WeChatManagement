using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace EasyAbp.WeChatManagement.Officials
{
    public class WeChatOfficialTokenExtensionGrant : ITokenExtensionGrant, ITransientDependency
    {
        public virtual string Name => WeChatOfficialConsts.GrantType;

        public virtual async Task<IActionResult> HandleAsync(ExtensionGrantContext context)
        {
            var identityOptions = context.HttpContext.RequestServices.GetRequiredService<IOptions<IdentityOptions>>();
            var weChatAppRepository = context.HttpContext.RequestServices.GetRequiredService<IWeChatAppRepository>();
            var OfficialLoginProviderProvider = context.HttpContext.RequestServices
                .GetRequiredService<IOfficialLoginProviderProvider>();
            var identityUserManager = context.HttpContext.RequestServices.GetRequiredService<IdentityUserManager>();
            var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();
            var scopeManager = context.HttpContext.RequestServices.GetRequiredService<IOpenIddictScopeManager>();
            var openIddictClaimDestinationsManager = context.HttpContext.RequestServices
                .GetRequiredService<AbpOpenIddictClaimDestinationsManager>();
            var identitySecurityLogManager =
                context.HttpContext.RequestServices.GetRequiredService<IdentitySecurityLogManager>();

            await identityOptions.SetAsync();

            var appId = context.Request.GetParameter("appid").ToString();
            var openId = context.Request.GetParameter("openid").ToString();
            var unionId = context.Request.GetParameter("unionid").ToString();


            if (string.IsNullOrWhiteSpace(appId))
            {
                return new ForbidResult(
                    new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] =
                            OpenIddictConstants.Errors.InvalidRequest,

                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            "请提供有效的 appid"
                    }!));
            }

            if (string.IsNullOrWhiteSpace(openId))
            {
                return new ForbidResult(
                    new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] =
                            OpenIddictConstants.Errors.InvalidRequest,

                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            "微信服务器没有提供有效的 openid"
                    }!));
            }

            var Official = await weChatAppRepository.GetOfficialAppByAppIdAsync(appId);

            string loginProvider;
            string providerKey;

            if (unionId.IsNullOrWhiteSpace())
            {
                loginProvider = await OfficialLoginProviderProvider.GetAppLoginProviderAsync(Official);
                providerKey = openId;
            }
            else
            {
                loginProvider = await OfficialLoginProviderProvider.GetOpenLoginProviderAsync(Official);
                providerKey = unionId;
            }

            var identityUser = await identityUserManager.FindByLoginAsync(loginProvider, providerKey);

            var principal = await signInManager.CreateUserPrincipalAsync(identityUser);

            principal.SetScopes(context.Request.GetScopes());
            principal.SetResources(await GetResourcesAsync(context.Request.GetScopes(), scopeManager));
            principal.SetClaim(WeChatOfficialConsts.AppIdClaim, appId); // 记录 appid

            await openIddictClaimDestinationsManager.SetAsync(principal);

            await identitySecurityLogManager.SaveAsync(
                new IdentitySecurityLogContext
                {
                    Identity = OpenIddictSecurityLogIdentityConsts.OpenIddict,
                    Action = OpenIddictSecurityLogActionConsts.LoginSucceeded,
                    UserName = context.Request.Username,
                    ClientId = context.Request.ClientId
                }
            );

            return new Microsoft.AspNetCore.Mvc.SignInResult(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                principal);
        }

        protected virtual async Task<IEnumerable<string>> GetResourcesAsync(ImmutableArray<string> scopes,
            IOpenIddictScopeManager scopeManager)
        {
            var resources = new List<string>();
            if (!scopes.Any())
            {
                return resources;
            }

            await foreach (var resource in scopeManager.ListResourcesAsync(scopes))
            {
                resources.Add(resource);
            }

            return resources;
        }
    }
}