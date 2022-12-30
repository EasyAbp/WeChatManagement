using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace EasyAbp.WeChatManagement.Officials
{
    public class NullOfficialLoginNewUserCreator : IOfficialLoginNewUserCreator
    {
        public virtual Task<IdentityUser> CreateAsync(string loginProvider, string providerKey)
        {
            throw new OfficialLoginMatchNoUserException();
        }
    }
}