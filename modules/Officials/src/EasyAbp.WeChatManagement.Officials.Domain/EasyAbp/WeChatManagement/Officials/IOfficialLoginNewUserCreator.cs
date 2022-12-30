using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace EasyAbp.WeChatManagement.Officials
{
    public interface IOfficialLoginNewUserCreator
    {
        Task<IdentityUser> CreateAsync(string loginProvider, string providerKey);
    }
}