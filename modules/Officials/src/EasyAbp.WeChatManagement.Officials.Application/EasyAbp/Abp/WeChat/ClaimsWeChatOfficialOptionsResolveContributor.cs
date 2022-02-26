using EasyAbp.Abp.WeChat.Official;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace EasyAbp.Abp.WeChat
{
    public class ClaimsWeChatOfficialOptionsResolveContributor : IWeChatOfficialOptionsResolveContributor
    {
        public const string ContributorName = "WeChatManagementClaims";

        public string Name => ContributorName;

        public void Resolve(WeChatOfficialResolveContext context)
        {
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

            var appid = currentUser.FindClaim("appid");

            if (appid == null || appid.Value.IsNullOrEmpty())
            {
                return;
            }

            // Todo: should use IOfficialStore
            var weChatAppRepository = context.ServiceProvider.GetRequiredService<IWeChatAppRepository>();

            var official = weChatAppRepository.GetOfficialAppByAppIdAsync(appid.Value).Result;

            context.Options = new AbpWeChatOfficialOptions
            {
                Token = official.Token,
                AppId = official.AppId,
                AppSecret = official.AppSecret,
                EncodingAesKey = official.EncodingAesKey,
                //OAuthRedirectUrl = official.OAuthRedirectUrl
            };
        }

        public async ValueTask ResolveAsync(WeChatOfficialResolveContext context)
        {
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

            var appid = currentUser.FindClaim("appid");

            if (appid == null || appid.Value.IsNullOrEmpty())
            {
                return;
            }

            // Todo: should use IOfficialStore
            var weChatAppRepository = context.ServiceProvider.GetRequiredService<IWeChatAppRepository>();

            var official = await weChatAppRepository.GetOfficialAppByAppIdAsync(appid.Value);

            context.Options = new AbpWeChatOfficialOptions
            {
                Token = official.Token,
                AppId = official.AppId,
                AppSecret = official.AppSecret,
                EncodingAesKey = official.EncodingAesKey,
                //OAuthRedirectUrl = official.OAuthRedirectUrl
            };
        }
    }
}
