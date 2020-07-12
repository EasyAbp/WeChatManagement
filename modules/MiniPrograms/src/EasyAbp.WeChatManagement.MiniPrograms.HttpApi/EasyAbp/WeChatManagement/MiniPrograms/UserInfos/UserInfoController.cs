using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    [RemoteService(Name = "EasyAbpWeChatManagementMiniPrograms")]
    [Route("/api/weChatManagement/miniPrograms/userInfo")]
    public class UserInfoController : MiniProgramsController, IUserInfoAppService
    {
        private readonly IUserInfoAppService _service;

        public UserInfoController(IUserInfoAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<UserInfoDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<UserInfoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}