using System;
using EasyAbp.WeChatManagement.Common.Localization;
using EasyAbp.WeChatManagement.Common.Permissions;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Common.WeChatAppUsers
{
    public class WeChatAppUserAppService : ReadOnlyAppService<WeChatAppUser, WeChatAppUserDto, Guid, PagedAndSortedResultRequestDto>,
        IWeChatAppUserAppService
    {
        protected override string GetPolicyName { get; set; } = CommonPermissions.WeChatAppUser.Default;
        protected override string GetListPolicyName { get; set; } = CommonPermissions.WeChatAppUser.Default;
        
        private readonly IWeChatAppUserRepository _repository;
        
        public WeChatAppUserAppService(IWeChatAppUserRepository repository) : base(repository)
        {
            _repository = repository;

            LocalizationResource = typeof(CommonResource);
            ObjectMapperContext = typeof(WeChatManagementCommonApplicationModule);
        }
    }
}