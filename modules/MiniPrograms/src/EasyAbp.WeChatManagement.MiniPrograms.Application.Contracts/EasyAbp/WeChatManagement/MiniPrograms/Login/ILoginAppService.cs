using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using IdentityModel.Client;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public interface ILoginAppService : IApplicationService
    {
        Task<TokenResponse> LoginAsync(LoginDto input);
        
        Task<TokenResponse> RefreshAsync(RefreshDto input);
    }
}