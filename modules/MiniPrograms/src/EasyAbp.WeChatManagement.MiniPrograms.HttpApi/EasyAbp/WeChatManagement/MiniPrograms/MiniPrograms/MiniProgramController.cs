using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    [RemoteService(Name = "EasyAbpWeChatManagementMiniPrograms")]
    [Route("/api/weChatManagement/miniPrograms/miniProgram")]
    public class MiniProgramController : MiniProgramsController, IMiniProgramAppService
    {
        private readonly IMiniProgramAppService _service;

        public MiniProgramController(IMiniProgramAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<MiniProgramDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<MiniProgramDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<MiniProgramDto> CreateAsync(CreateUpdateMiniProgramDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<MiniProgramDto> UpdateAsync(Guid id, CreateUpdateMiniProgramDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }
    }
}