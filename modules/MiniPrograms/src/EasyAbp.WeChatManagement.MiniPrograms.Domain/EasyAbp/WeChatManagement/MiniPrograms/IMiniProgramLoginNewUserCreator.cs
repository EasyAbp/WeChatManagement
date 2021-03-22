using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using IdentityServer4.Validation;
using Volo.Abp.Identity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public interface IMiniProgramLoginNewUserCreator
    {
        Task<IdentityUser> CreateAsync(string loginProvider, string providerKey);
    }
}