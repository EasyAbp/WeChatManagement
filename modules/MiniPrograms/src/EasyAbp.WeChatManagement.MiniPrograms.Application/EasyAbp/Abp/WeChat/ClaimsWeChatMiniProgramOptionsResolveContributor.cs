using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Users;

namespace EasyAbp.Abp.WeChat
{
    public class ClaimsWeChatMiniProgramOptionsResolveContributor : IWeChatMiniProgramOptionsResolveContributor
    {
        public string Name { get; } = "WeChatManagementClaims";

        public async Task ResolveAsync(WeChatMiniProgramOptionsResolveContext context)
        {
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
            
            var appid = currentUser.FindClaim("appid");

            if (appid == null || appid.Value.IsNullOrEmpty())
            {
                return;
            }
            
            // Todo: should use IMiniProgramStore
            var miniProgramRepository = context.ServiceProvider.GetRequiredService<IMiniProgramRepository>();

            var miniProgram = await miniProgramRepository.GetAsync(x => x.AppId == appid.Value);
            
            context.Options = new AbpWeChatMiniProgramOptions
            {
                OpenAppId = miniProgram.OpenAppIdOrName,
                AppId = miniProgram.AppId,
                AppSecret = miniProgram.AppSecret,
                EncodingAesKey = miniProgram.EncodingAesKey,
                Token = miniProgram.Token
            };
        }
    }
}