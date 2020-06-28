using System.Threading.Tasks;
using IdentityServer4.Validation;
using Volo.Abp.Identity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public interface IMiniProgramLoginNewUserCreator
    {
        Task<IdentityUser> CreateAsync(ExtensionGrantValidationContext context);
    }
}