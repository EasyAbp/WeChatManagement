using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms.Dtos;
using EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.MiniPrograms.MiniProgram.ViewModels;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.MiniPrograms.MiniProgram
{
    public class EditModalModel : MiniProgramsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditMiniProgramViewModel ViewModel { get; set; }

        private readonly IMiniProgramAppService _service;

        public EditModalModel(IMiniProgramAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<MiniProgramDto, CreateEditMiniProgramViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditMiniProgramViewModel, CreateUpdateMiniProgramDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}