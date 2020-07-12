using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    [RemoteService(Name = "EasyAbpWeChatManagementMiniPrograms")]
    [Route("/api/weChatManagement/miniPrograms/login")]
    public class LoginController : MiniProgramsController, ILoginAppService
    {
        private readonly ILoginAppService _service;

        public LoginController(ILoginAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("login")]
        public Task<TokenResponse> LoginAsync(LoginDto input)
        {
            return _service.LoginAsync(input);
        }

        [HttpPost]
        [Route("refresh")]
        public Task<TokenResponse> RefreshAsync(RefreshDto input)
        {
            return _service.RefreshAsync(input);
        }
    }
}