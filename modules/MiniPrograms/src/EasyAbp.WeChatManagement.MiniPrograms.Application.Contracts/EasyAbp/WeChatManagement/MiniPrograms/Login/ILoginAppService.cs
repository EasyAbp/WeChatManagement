using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using JetBrains.Annotations;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public interface ILoginAppService : IApplicationService
    {
        Task<LoginOutput> LoginAsync(LoginInput input);
        
        Task<string> RefreshAsync(RefreshInput input);

        Task<GetPcLoginACodeOutput> GetPcLoginACodeAsync([NotNull] string miniProgramName,
            [CanBeNull] string handlePage = null);
        
        Task AuthorizePcAsync(AuthorizePcInput input);
        
        Task<PcLoginOutput> PcLoginAsync(PcLoginInput input);
        
        Task<PcLoginRequestTokensOutput> PcLoginRequestTokensAsync(PcLoginInput input);

        Task BindAsync(LoginInput input);

        Task UnbindAsync(LoginInput input);
    }
}