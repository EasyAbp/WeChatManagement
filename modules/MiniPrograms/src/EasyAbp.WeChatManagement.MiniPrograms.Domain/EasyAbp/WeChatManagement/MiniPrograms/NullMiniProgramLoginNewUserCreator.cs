using System.Threading.Tasks;
using IdentityServer4.Validation;
using Volo.Abp;
using Volo.Abp.Identity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class NullMiniProgramLoginNewUserCreator : IMiniProgramLoginNewUserCreator
    {
        public virtual Task<IdentityUser> CreateAsync(ExtensionGrantValidationContext context, string loginProvider, string providerKey)
        {
            throw new MiniProgramLoginMatchNoUserException();
        }
    }
}