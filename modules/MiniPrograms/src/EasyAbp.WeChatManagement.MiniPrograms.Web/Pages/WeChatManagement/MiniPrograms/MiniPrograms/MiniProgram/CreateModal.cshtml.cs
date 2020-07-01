using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms.Dtos;
using EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.MiniPrograms.MiniProgram.ViewModels;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.MiniPrograms.MiniProgram
{
    public class CreateModalModel : MiniProgramsPageModel
    {
        [BindProperty]
        public CreateEditMiniProgramViewModel ViewModel { get; set; } = new CreateEditMiniProgramViewModel();

        private readonly IMiniProgramAppService _service;

        public CreateModalModel(IMiniProgramAppService service)
        {
            _service = service;
        }
        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditMiniProgramViewModel, CreateUpdateMiniProgramDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}