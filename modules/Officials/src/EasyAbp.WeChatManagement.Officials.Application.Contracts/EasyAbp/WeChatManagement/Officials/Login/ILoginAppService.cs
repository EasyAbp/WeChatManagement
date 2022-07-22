using EasyAbp.WeChatManagement.Officials.Login.Dtos;
using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Officials.Login
{
    public interface ILoginAppService : IApplicationService
    {
        Task<LoginOutput> LoginAsync(LoginInput input);

        Task<GetLoginAuthorizeUrlOutput> GetLoginAuthorizeUrlAsync([NotNull] string officialName, [CanBeNull] string handlePage = null);

        Task BindAsync(LoginInput input);

        Task UnbindAsync(LoginInput input);
    }
}
