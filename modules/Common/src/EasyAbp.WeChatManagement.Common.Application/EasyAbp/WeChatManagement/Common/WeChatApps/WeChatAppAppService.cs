using System;
using EasyAbp.WeChatManagement.Common.Permissions;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Common.WeChatApps
{
    public class WeChatAppAppService : CrudAppService<WeChatApp, WeChatAppDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateWeChatAppDto, CreateUpdateWeChatAppDto>,
        IWeChatAppAppService
    {
        protected override string GetPolicyName { get; set; } = CommonPermissions.WeChatApp.Default;
        protected override string GetListPolicyName { get; set; } = CommonPermissions.WeChatApp.Default;
        protected override string CreatePolicyName { get; set; } = CommonPermissions.WeChatApp.Create;
        protected override string UpdatePolicyName { get; set; } = CommonPermissions.WeChatApp.Update;
        protected override string DeletePolicyName { get; set; } = CommonPermissions.WeChatApp.Delete;
        private readonly IWeChatAppRepository _repository;
        
        public WeChatAppAppService(IWeChatAppRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}