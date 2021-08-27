using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.Web.Pages.WeChatManagement.Common.WeChatApps.WeChatApp.ViewModels;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.WeChatManagement.Common.Web.Pages.WeChatManagement.Common.WeChatApps.WeChatApp
{
    public class EditModalModel : CommonPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditWeChatAppViewModel ViewModel { get; set; }

        private readonly IWeChatAppAppService _appService;

        public EditModalModel(IWeChatAppAppService appService)
        {
            _appService = appService;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _appService.GetAsync(Id);
            ViewModel = ObjectMapper.Map<WeChatAppDto, CreateEditWeChatAppViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditWeChatAppViewModel, CreateUpdateWeChatAppDto>(ViewModel);
            await _appService.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}