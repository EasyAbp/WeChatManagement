using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Permissions;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

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
        }
    }
}