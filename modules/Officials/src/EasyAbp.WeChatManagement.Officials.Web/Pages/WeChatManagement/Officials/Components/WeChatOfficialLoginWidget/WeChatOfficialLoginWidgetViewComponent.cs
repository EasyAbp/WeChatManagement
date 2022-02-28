using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Officials.Login;
using EasyAbp.WeChatManagement.Officials.Login.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace EasyAbp.WeChatManagement.Officials.Web.Pages.WeChatManagement.Officials.Components.WeChatOfficialLoginWidget
{
    [ViewComponent(Name = "WeChatOfficialLogin")]
    [Widget(
        ScriptTypes = new[] { typeof(WeChatOfficialLoginScriptBundleContributor) },
        StyleTypes = new[] { typeof(WeChatOfficialLoginStyleBundleContributor) }
    )]
    public class WeChatOfficialLoginWidgetViewComponent : AbpViewComponent
    {
        private readonly ILoginAppService _loginAppService;

        public WeChatOfficialLoginWidgetViewComponent(ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string officialName)
        {
            GetLoginAuthorizeUrlOutput authorizeUrl;
            try
            {
                authorizeUrl = await _loginAppService.GetLoginAuthorizeUrlAsync(officialName);
            }
            catch
            {
                authorizeUrl = new GetLoginAuthorizeUrlOutput
                {
                    AuthorizeUrl = null
                };
            }

            var viewModel = new WeChatOfficialLoginViewModel
            {
                OfficialName = officialName,
                AuthorizeUrl = authorizeUrl.AuthorizeUrl
            };

            return View(
                "~/Pages/WeChatManagement/Officials/Components/WeChatOfficialLoginWidget/Default.cshtml",
                viewModel);
        }
    }
}