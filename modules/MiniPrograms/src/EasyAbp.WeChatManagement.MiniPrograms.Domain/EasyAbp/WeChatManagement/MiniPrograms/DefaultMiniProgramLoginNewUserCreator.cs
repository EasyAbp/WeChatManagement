using System.Threading.Tasks;
using IdentityServer4.Validation;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [Dependency(TryRegister = true)]
    public class DefaultMiniProgramLoginNewUserCreator : IMiniProgramLoginNewUserCreator, ITransientDependency
    {
        public virtual Task<IdentityUser> CreateAsync(ExtensionGrantValidationContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}