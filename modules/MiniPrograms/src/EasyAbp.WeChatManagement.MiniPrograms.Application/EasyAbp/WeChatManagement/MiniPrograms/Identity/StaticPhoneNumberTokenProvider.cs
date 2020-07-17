using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace EasyAbp.WeChatManagement.MiniPrograms.Identity
{
    public class StaticPhoneNumberTokenProvider : PhoneNumberTokenProvider<IdentityUser>
    {
        private const string Token = "000000";
        
        public override Task<string> GenerateAsync(string purpose, UserManager<IdentityUser> manager, IdentityUser user)
        {
            return Task.FromResult(Token);
        }

        public override Task<bool> ValidateAsync(string purpose, string token, UserManager<IdentityUser> manager, IdentityUser user)
        {
            return Task.FromResult(token == Token);
        }
    }
}