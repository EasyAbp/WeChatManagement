using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using IdentityServer4.Validation;
using Volo.Abp;
using Volo.Abp.Identity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class NullMiniProgramLoginNewUserCreator : IMiniProgramLoginNewUserCreator
    {
        public virtual Task<IdentityUser> CreateAsync(string loginProvider, string providerKey)
        {
            throw new MiniProgramLoginMatchNoUserException();
        }
    }
}