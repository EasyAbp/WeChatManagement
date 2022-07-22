using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Officials.Permissions;
using EasyAbp.WeChatManagement.Officials.UserInfos.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace EasyAbp.WeChatManagement.Officials.UserInfos
{
    public class UserInfoAppService : ReadOnlyAppService<UserInfo, UserInfoDto, Guid, PagedAndSortedResultRequestDto>,
        IUserInfoAppService
    {
        protected override string GetPolicyName { get; set; } = OfficialsPermissions.UserInfo.Default;
        protected override string GetListPolicyName { get; set; } = OfficialsPermissions.UserInfo.Default;

        private readonly IUserInfoRepository _repository;
        
        public UserInfoAppService(IUserInfoRepository repository) : base(repository)
        {
            _repository = repository;
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