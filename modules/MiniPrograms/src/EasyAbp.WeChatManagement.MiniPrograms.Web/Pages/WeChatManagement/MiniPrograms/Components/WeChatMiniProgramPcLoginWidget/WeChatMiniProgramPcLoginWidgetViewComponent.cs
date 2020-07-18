using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Login;
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
            var aCode = await _loginAppService.GetPcLoginACodeAsync(miniProgramName);
            
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