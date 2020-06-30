using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Dtos;
using IdentityModel.Client;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public interface ILoginAppService : IApplicationService
    {
        Task<TokenResponse> LoginAsync(LoginDto input);
        
        Task<TokenResponse> RefreshAsync(RefreshDto input);
    }
}