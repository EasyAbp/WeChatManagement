using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.Web.Pages.WeChatManagement.Common.WeChatApps.WeChatApp.ViewModels;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.WeChatManagement.Common.Web.Pages.WeChatManagement.Common.WeChatApps.WeChatApp
{
    public class CreateModalModel : CommonPageModel
    {
        [BindProperty]
        public CreateWeChatAppViewModel ViewModel { get; set; } = new CreateWeChatAppViewModel();

        private readonly IWeChatAppAppService _appService;

        public CreateModalModel(IWeChatAppAppService appService)
        {
            _appService = appService;
        }
        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateWeChatAppViewModel, CreateWeChatAppDto>(ViewModel);
            await _appService.CreateAsync(dto);
            return NoContent();
        }
    }
}