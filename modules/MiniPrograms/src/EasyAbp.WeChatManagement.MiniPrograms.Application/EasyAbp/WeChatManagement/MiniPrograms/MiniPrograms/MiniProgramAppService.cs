using System;
using EasyAbp.WeChatManagement.MiniPrograms.Permissions;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public class MiniProgramAppService : CrudAppService<MiniProgram, MiniProgramDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMiniProgramDto, CreateUpdateMiniProgramDto>,
        IMiniProgramAppService
    {
        protected override string GetPolicyName { get; set; } = MiniProgramsPermissions.MiniProgram.Default;
        protected override string GetListPolicyName { get; set; } = MiniProgramsPermissions.MiniProgram.Default;
        protected override string CreatePolicyName { get; set; } = MiniProgramsPermissions.MiniProgram.Create;
        protected override string UpdatePolicyName { get; set; } = MiniProgramsPermissions.MiniProgram.Update;
        protected override string DeletePolicyName { get; set; } = MiniProgramsPermissions.MiniProgram.Delete;
        private readonly IMiniProgramRepository _repository;
        
        public MiniProgramAppService(IMiniProgramRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}