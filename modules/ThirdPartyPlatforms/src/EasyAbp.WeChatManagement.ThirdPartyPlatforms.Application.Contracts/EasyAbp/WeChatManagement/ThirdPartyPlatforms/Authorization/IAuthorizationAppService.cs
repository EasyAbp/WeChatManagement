using System.Threading.Tasks;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization;

public interface IAuthorizationAppService : IApplicationService
{
    Task<PreAuthResultDto> PreAuthAsync(PreAuthInputDto input);
    
    Task<HandleCallbackResultDto> HandleCallbackAsync(HandleCallbackInputDto input);
}