using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.Officials.Login.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.Officials.Login
{
    [RemoteService(Name = WeChatManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/wechat-management/mini-programs/login")]
    public class LoginController : OfficialsController, ILoginAppService
    {
        private readonly ILoginAppService _service;

        public LoginController(ILoginAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("login")]
        public Task<LoginOutput> LoginAsync(LoginInput input)
        {
            return _service.LoginAsync(input);
        }

        [HttpPost]
        [Route("refresh")]
        public Task<string> RefreshAsync(RefreshInput input)
        {
            return _service.RefreshAsync(input);
        }

        [HttpPost]
        [Route("bind")]
        public Task BindAsync(LoginInput input)
        {
            return _service.BindAsync(input);
        }

        [HttpPost]
        [Route("unbind")]
        public Task UnbindAsync(LoginInput input)
        {
            return _service.UnbindAsync(input);
        }
    }
}