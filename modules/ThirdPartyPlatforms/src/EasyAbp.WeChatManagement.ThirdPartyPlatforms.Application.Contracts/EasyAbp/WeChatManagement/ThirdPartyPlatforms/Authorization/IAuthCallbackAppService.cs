using System.Threading.Tasks;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization;

public interface IAuthCallbackAppService : IApplicationService
{
    Task<HandleAuthCallbackResultDto> HandleAsync(HandleAuthCallbackInputDto input);
}