using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Officials.Login.Dtos;
using JetBrains.Annotations;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Officials.Login
{
    public interface ILoginAppService : IApplicationService
    {
        Task<LoginOutput> LoginAsync(LoginInput input);

        Task<string> RefreshAsync(RefreshInput input);

        Task BindAsync(LoginInput input);

        Task UnbindAsync(LoginInput input);
    }
}