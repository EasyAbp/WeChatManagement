using System.Threading.Tasks;
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