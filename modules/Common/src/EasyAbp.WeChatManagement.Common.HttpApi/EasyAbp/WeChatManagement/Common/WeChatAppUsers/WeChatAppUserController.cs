using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.WeChatManagement.Common.WeChatAppUsers
{
    [RemoteService(Name = WeChatManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/wechat-management/common/wechat-app-user")]
    public class WeChatAppUserController : CommonController, IWeChatAppUserAppService
    {
        private readonly IWeChatAppUserAppService _service;

        public WeChatAppUserController(IWeChatAppUserAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<WeChatAppUserDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<WeChatAppUserDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}