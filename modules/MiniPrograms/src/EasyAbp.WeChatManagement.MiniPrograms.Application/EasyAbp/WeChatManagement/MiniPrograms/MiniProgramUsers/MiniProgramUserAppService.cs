using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Permissions;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers
{
    public class MiniProgramUserAppService : CrudAppService<MiniProgramUser, MiniProgramUserDto, Guid, PagedAndSortedResultRequestDto, object, object>,
        IMiniProgramUserAppService
    {
        protected override string GetPolicyName { get; set; } = MiniProgramsPermissions.MiniProgramUser.Default;
        protected override string GetListPolicyName { get; set; } = MiniProgramsPermissions.MiniProgramUser.Default;
        
        private readonly IMiniProgramUserRepository _repository;
        
        public MiniProgramUserAppService(IMiniProgramUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [RemoteService(false)]
        public override Task<MiniProgramUserDto> CreateAsync(object input)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task<MiniProgramUserDto> UpdateAsync(Guid id, object input)
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