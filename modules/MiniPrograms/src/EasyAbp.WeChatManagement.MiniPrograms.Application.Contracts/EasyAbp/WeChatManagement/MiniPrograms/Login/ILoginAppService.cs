using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public interface ILoginAppService : IApplicationService
    {
        Task<string> LoginAsync(LoginDto input);
        
        Task<string> RefreshAsync(RefreshDto input);
    }
}