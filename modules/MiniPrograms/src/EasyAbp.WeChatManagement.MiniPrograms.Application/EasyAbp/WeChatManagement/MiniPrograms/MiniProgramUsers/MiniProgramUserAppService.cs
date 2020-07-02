using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Permissions;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers
{
    public class MiniProgramUserAppService : ReadOnlyAppService<MiniProgramUser, MiniProgramUserDto, Guid, PagedAndSortedResultRequestDto>,
        IMiniProgramUserAppService
    {
        protected override string GetPolicyName { get; set; } = MiniProgramsPermissions.MiniProgramUser.Default;
        protected override string GetListPolicyName { get; set; } = MiniProgramsPermissions.MiniProgramUser.Default;
        
        private readonly IMiniProgramUserRepository _repository;
        
        public MiniProgramUserAppService(IMiniProgramUserRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}