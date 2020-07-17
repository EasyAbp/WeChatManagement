using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Identity.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.Identity
{
    public interface IProfileAppService : IApplicationService
    {
        Task BindPhoneNumberAsync(BindPhoneNumberInput input);
    }
}