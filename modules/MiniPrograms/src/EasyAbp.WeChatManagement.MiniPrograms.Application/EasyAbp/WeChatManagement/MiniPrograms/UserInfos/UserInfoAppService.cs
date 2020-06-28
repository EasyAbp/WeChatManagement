using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Permissions;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public class UserInfoAppService : CrudAppService<UserInfo, UserInfoDto, Guid, PagedAndSortedResultRequestDto, object, object>,
        IUserInfoAppService
    {
        protected override string GetPolicyName { get; set; } = MiniProgramsPermissions.UserInfo.Default;
        protected override string GetListPolicyName { get; set; } = MiniProgramsPermissions.UserInfo.Default;

        private readonly IUserInfoRepository _repository;
        
        public UserInfoAppService(IUserInfoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [RemoteService(false)]
        public override Task<UserInfoDto> CreateAsync(object input)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task<UserInfoDto> UpdateAsync(Guid id, object input)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task DeleteAsync(Guid id)
        {
            throw new NotSupportedException();
        }
    }
}