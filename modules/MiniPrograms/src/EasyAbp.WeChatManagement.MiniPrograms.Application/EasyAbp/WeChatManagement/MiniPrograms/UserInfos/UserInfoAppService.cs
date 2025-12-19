using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using EasyAbp.WeChatManagement.MiniPrograms.Permissions;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public class UserInfoAppService : ReadOnlyAppService<UserInfo, UserInfoDto, Guid, PagedAndSortedResultRequestDto>,
        IUserInfoAppService
    {
        protected override string GetPolicyName { get; set; } = MiniProgramsPermissions.UserInfo.Default;
        protected override string GetListPolicyName { get; set; } = MiniProgramsPermissions.UserInfo.Default;

        private readonly IUserInfoRepository _repository;
        
        public UserInfoAppService(IUserInfoRepository repository) : base(repository)
        {
            _repository = repository;

            LocalizationResource = typeof(MiniProgramsResource);
            ObjectMapperContext = typeof(WeChatManagementMiniProgramsApplicationModule);
        }

        [Authorize]
        public async Task<UserInfoDto> UpdateAsync(UserInfoModel input)
        {
            var userInfo = await _repository.FindAsync(x => x.UserId == CurrentUser.GetId());
            
            userInfo.UpdateInfo(input);

            await _repository.UpdateAsync(userInfo, true);

            return await MapToGetOutputDtoAsync(userInfo);
        }
    }
}