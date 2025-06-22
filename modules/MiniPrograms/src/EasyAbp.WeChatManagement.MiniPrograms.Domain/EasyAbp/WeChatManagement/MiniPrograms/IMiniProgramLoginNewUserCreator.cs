using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public interface IMiniProgramLoginNewUserCreator
    {
        Task<IdentityUser> CreateAsync(string loginProvider, string providerKey, string phoneNumber = null);
    }
}