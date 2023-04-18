using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Login;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.Components.WeChatMiniProgramPcBindWidget
{
    [ViewComponent(Name = "WeChatMiniProgramPcBind")]
    [Widget(
        ScriptTypes = new[] { typeof(WeChatMiniProgramPcBindScriptBundleContributor) },
        StyleTypes = new[] { typeof(WeChatMiniProgramPcBindStyleBundleContributor) }
    )]
    public class WeChatMiniProgramPcBindWidgetViewComponent : AbpViewComponent
    {
        private readonly ILoginAppService _loginAppService;

        public WeChatMiniProgramPcBindWidgetViewComponent(ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="miniProgramName"></param>
        /// <param name="codeType">二维码类型</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(string miniProgramName)
        {
            GetPcBindACodeOutput aCode;
            try
            {
                aCode = await _loginAppService.GetPcBindACodeAsync(miniProgramName);
            }
            catch
            {
                aCode = new GetPcBindACodeOutput
                {
                    ACode = null,
                    Token = null
                };
            }

            var viewModel = new WeChatMiniProgramPcBindViewModel
            {
                MiniProgramName = miniProgramName,
                Token = aCode.Token,
                ACode = aCode.ACode,
                HasBound = aCode.HasBound,
                NickName = aCode.NickName,
                AvatarUrl = aCode.AvatarUrl,
            };

            return View(
                "~/Pages/WeChatManagement/MiniPrograms/Components/WeChatMiniProgramPcBindWidget/Default.cshtml",
                viewModel);
        }
    }
}
