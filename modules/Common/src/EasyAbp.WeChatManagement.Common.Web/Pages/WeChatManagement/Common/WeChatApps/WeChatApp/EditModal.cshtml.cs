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
        public EditWeChatAppViewModel ViewModel { get; set; }

        private readonly IWeChatAppAppService _appService;

        public EditModalModel(IWeChatAppAppService appService)
        {
            _appService = appService;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _appService.GetAsync(Id);
            ViewModel = ObjectMapper.Map<WeChatAppDto, EditWeChatAppViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditWeChatAppViewModel, UpdateWeChatAppDto>(ViewModel);
            await _appService.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}