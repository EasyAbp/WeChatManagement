using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
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
        private readonly IdentityUserManager _identityUserManager;

        public DefaultMiniProgramLoginNewUserCreator(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IdentityUserManager identityUserManager)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _identityUserManager = identityUserManager;
        }
        
        public virtual async Task<IdentityUser> CreateAsync(UserInfoModel userInfoModel, string loginProvider, string providerKey)
        {
            var identityUser = new IdentityUser(_guidGenerator.Create(), await GenerateUserNameAsync(userInfoModel),
                await GenerateEmailAsync(userInfoModel), _currentTenant.Id);
            
            CheckIdentityResult(await _identityUserManager.CreateAsync(identityUser));

            CheckIdentityResult(await _identityUserManager.AddDefaultRolesAsync(identityUser));

            CheckIdentityResult(await _identityUserManager.AddLoginAsync(identityUser,
                new UserLoginInfo(loginProvider, providerKey, "微信用户")));
            
            return identityUser;
        }

        protected virtual void CheckIdentityResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new AbpIdentityResultException(result);
            }
        }
        
        protected virtual Task<string> GenerateUserNameAsync(UserInfoModel userInfoModel)
        {
            return Task.FromResult("WeChat_" + Guid.NewGuid());
        }
        
        protected virtual Task<string> GenerateEmailAsync(UserInfoModel userInfoModel)
        {
            return Task.FromResult(Guid.NewGuid() + "@fake-email.com");
        }

    }
}