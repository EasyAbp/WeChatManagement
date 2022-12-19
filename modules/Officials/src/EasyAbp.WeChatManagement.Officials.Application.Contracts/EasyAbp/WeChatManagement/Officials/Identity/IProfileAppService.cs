using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Officials.Identity.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Officials.Identity
{
    public interface IProfileAppService : IApplicationService
    {
        Task BindPhoneNumberAsync(BindPhoneNumberInput input);
    }
}