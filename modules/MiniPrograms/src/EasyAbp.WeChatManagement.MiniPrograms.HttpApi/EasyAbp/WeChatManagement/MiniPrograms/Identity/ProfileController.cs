using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Identity.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Identity
{
    [RemoteService(Name = "EasyAbpWeChatManagementMiniPrograms")]
    [Route("/api/weChatManagement/miniPrograms/profile")]
    public class ProfileController : MiniProgramsController, IProfileAppService
    {
        private readonly IProfileAppService _service;

        public ProfileController(IProfileAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 通过微信开放能力获取并给当前用户绑定手机号，更新信息：https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/getPhoneNumber.html
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("bindPhoneNumber")]
        public Task BindPhoneNumberAsync(BindPhoneNumberInput input)
        {
            return _service.BindPhoneNumberAsync(input);
        }
    }
}