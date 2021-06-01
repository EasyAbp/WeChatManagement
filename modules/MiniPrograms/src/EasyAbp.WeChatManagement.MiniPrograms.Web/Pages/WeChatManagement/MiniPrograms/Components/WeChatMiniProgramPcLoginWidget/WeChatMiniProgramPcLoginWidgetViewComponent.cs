using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Login;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.Components.WeChatMiniProgramPcLoginWidget
{
    [ViewComponent(Name = "WeChatMiniProgramPcLogin")]
    [Widget(
        ScriptTypes = new[] {typeof(WeChatMiniProgramPcLoginScriptBundleContributor)},
        StyleTypes = new[] {typeof(WeChatMiniProgramPcLoginStyleBundleContributor)}
    )]
    public class WeChatMiniProgramPcLoginWidgetViewComponent : AbpViewComponent
    {
        private readonly ILoginAppService _loginAppService;

        public WeChatMiniProgramPcLoginWidgetViewComponent(ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(string miniProgramName)
        {
            GetPcLoginACodeOutput aCode;
            try
            {
                aCode = await _loginAppService.GetPcLoginACodeAsync(miniProgramName);
            }
            catch
            {
                aCode = new GetPcLoginACodeOutput
                {
                    ACode = null,
                    Token = null
                };
            }
            
            var viewModel = new WeChatMiniProgramPcLoginViewModel
            {
                MiniProgramName = miniProgramName,
                Token = aCode.Token,
                ACode = aCode.ACode
            };

            return View(
                "~/Pages/WeChatManagement/MiniPrograms/Components/WeChatMiniProgramPcLoginWidget/Default.cshtml",
                viewModel);
        }
    }
}