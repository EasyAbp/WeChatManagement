using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [Dependency(TryRegister = true)]
    public class DefaultMiniProgramLoginNewUserCreator : IMiniProgramLoginNewUserCreator, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IdentityUserManager _identityUserManager;

        public DefaultMiniProgramLoginNewUserCreator(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IOptions<IdentityOptions> identityOptions,
            IdentityUserManager identityUserManager)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _identityOptions = identityOptions;
            _identityUserManager = identityUserManager;
        }

        public virtual async Task<IdentityUser> CreateAsync(string loginProvider, string providerKey, string phoneNumber = null)
        {
            await _identityOptions.SetAsync();

            var identityUser = new IdentityUser(_guidGenerator.Create(), await GenerateUserNameAsync(),
                await GenerateEmailAsync(), _currentTenant.Id);

            if (!phoneNumber.IsNullOrEmpty())
            {
                identityUser.SetPhoneNumber(phoneNumber, true);
            }

            (await _identityUserManager.CreateAsync(identityUser)).CheckErrors();

            (await _identityUserManager.AddDefaultRolesAsync(identityUser)).CheckErrors();

            (await _identityUserManager.AddLoginAsync(identityUser,
                new UserLoginInfo(loginProvider, providerKey,
                    WeChatManagementCommonConsts.WeChatUserLoginInfoDisplayName))).CheckErrors();

            return identityUser;
        }

        protected virtual Task<string> GenerateUserNameAsync()
        {
            return Task.FromResult("WeChat_" + Guid.NewGuid());
        }

        protected virtual Task<string> GenerateEmailAsync()
        {
            return Task.FromResult(Guid.NewGuid() + "@fake-email.com");
        }

    }
}