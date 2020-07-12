using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers
{
    [RemoteService(Name = "EasyAbpWeChatManagementMiniPrograms")]
    [Route("/api/weChatManagement/miniPrograms/miniProgramUser")]
    public class MiniProgramUserController : MiniProgramsController, IMiniProgramUserAppService
    {
        private readonly IMiniProgramUserAppService _service;

        public MiniProgramUserController(IMiniProgramUserAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<MiniProgramUserDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<MiniProgramUserDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}