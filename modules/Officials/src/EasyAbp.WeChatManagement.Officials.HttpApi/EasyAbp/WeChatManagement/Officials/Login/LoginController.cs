using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.Officials.Login.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.Officials.Login
{
    [RemoteService(Name = WeChatManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/wechat-management/officials/login")]
    public class LoginController : OfficialsController, ILoginAppService
    {
        private readonly ILoginAppService _service;

        public LoginController(ILoginAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get-login-authorize-url")]
        public Task<GetLoginAuthorizeUrlOutput> GetLoginAuthorizeUrlAsync(string officialName, string handlePage = null)
        {
            return _service.GetLoginAuthorizeUrlAsync(officialName, handlePage);
        }

        [HttpPost]
        [Route("login")]
        public Task<LoginOutput> LoginAsync(LoginInput input)
        {
            return _service.LoginAsync(input);
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
