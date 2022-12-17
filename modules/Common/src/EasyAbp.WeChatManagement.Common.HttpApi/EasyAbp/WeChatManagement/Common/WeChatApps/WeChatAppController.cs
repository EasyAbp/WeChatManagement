using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.WeChatManagement.Common.WeChatApps
{
    [RemoteService(Name = WeChatManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/wechat-management/common/wechat-app")]
    public class WeChatAppController : CommonController, IWeChatAppAppService
    {
        private readonly IWeChatAppAppService _appService;

        public WeChatAppController(IWeChatAppAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<WeChatAppDto> GetAsync(Guid id)
        {
            return _appService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<WeChatAppDto>> GetListAsync(WeChatAppGetListInput input)
        {
            return _appService.GetListAsync(input);
        }

        [HttpPost]
        public Task<WeChatAppDto> CreateAsync(CreateWeChatAppDto input)
        {
            return _appService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<WeChatAppDto> UpdateAsync(Guid id, UpdateWeChatAppDto input)
        {
            return _appService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _appService.DeleteAsync(id);
        }
    }
}