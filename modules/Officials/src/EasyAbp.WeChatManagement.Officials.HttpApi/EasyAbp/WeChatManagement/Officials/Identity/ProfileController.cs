using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.Officials.Identity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.Officials.Identity
{
    [RemoteService(Name = WeChatManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/wechat-management/mini-programs/profile")]
    public class ProfileController : OfficialsController, IProfileAppService
    {
        private readonly IProfileAppService _service;

        public ProfileController(IProfileAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 通过微信开放能力获取并给当前用户绑定手机号，更新信息：https://developers.weixin.qq.com/Official/dev/framework/open-ability/getPhoneNumber.html
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("bind-phone-number")]
        public Task BindPhoneNumberAsync(BindPhoneNumberInput input)
        {
            return _service.BindPhoneNumberAsync(input);
        }
    }
}