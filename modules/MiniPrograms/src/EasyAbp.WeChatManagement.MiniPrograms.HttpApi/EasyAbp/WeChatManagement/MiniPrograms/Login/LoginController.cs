using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    [RemoteService(Name = WeChatManagementRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/wechat-management/mini-programs/login")]
    public class LoginController : MiniProgramsController, ILoginAppService
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

        [HttpGet]
        [Route("pc-login-acode/{miniProgramName}")]
        public Task<GetPcLoginACodeOutput> GetPcLoginACodeAsync(string miniProgramName, string handlePage = null)
        {
            return _service.GetPcLoginACodeAsync(miniProgramName, handlePage);
        }

        [HttpPost]
        [Route("authorize-pc")]
        public Task AuthorizePcAsync(AuthorizePcInput input)
        {
            return _service.AuthorizePcAsync(input);
        }

        [HttpPost]
        [Route("pc-login")]
        public Task<PcLoginOutput> PcLoginAsync(PcLoginInput input)
        {
            return _service.PcLoginAsync(input);
        }

        [HttpPost]
        [Route("pc-login-request-tokens")]
        public Task<PcLoginRequestTokensOutput> PcLoginRequestTokensAsync(PcLoginInput input)
        {
            return _service.PcLoginRequestTokensAsync(input);
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