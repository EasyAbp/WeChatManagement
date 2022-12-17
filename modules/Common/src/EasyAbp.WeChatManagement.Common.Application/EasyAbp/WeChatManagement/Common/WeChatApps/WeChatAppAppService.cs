using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.Permissions;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Common.WeChatApps;

public class WeChatAppAppService : CrudAppService<WeChatApp, WeChatAppDto, Guid, WeChatAppGetListInput,
    CreateWeChatAppDto, UpdateWeChatAppDto>, IWeChatAppAppService
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

    protected override async Task<IQueryable<WeChatApp>> CreateFilteredQueryAsync(WeChatAppGetListInput input)
    {
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.Type.HasValue, x => x.Type == input.Type)
            .WhereIf(input.ComponentWeChatAppId.HasValue, x => x.ComponentWeChatAppId == input.ComponentWeChatAppId)
            .WhereIf(!input.OpenAppIdOrName.IsNullOrWhiteSpace(), x => x.OpenAppIdOrName == input.OpenAppIdOrName)
            .WhereIf(!input.AppId.IsNullOrWhiteSpace(), x => x.AppId == input.AppId)
            .WhereIf(input.IsStatic.HasValue, x => x.IsStatic == input.IsStatic);
    }

    protected override async Task<WeChatApp> MapToEntityAsync(CreateWeChatAppDto createInput)
    {
        return new WeChatApp(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            createInput.Type,
            createInput.ComponentWeChatAppId,
            createInput.Name,
            createInput.DisplayName,
            createInput.OpenAppIdOrName,
            createInput.AppId,
            createInput.AppSecret,
            createInput.Token,
            createInput.EncodingAesKey,
            false);
    }

    protected override Task MapToEntityAsync(UpdateWeChatAppDto updateInput, WeChatApp entity)
    {
        entity.Update(
            updateInput.ComponentWeChatAppId,
            updateInput.Name,
            updateInput.DisplayName,
            updateInput.OpenAppIdOrName,
            updateInput.AppId,
            updateInput.AppSecret,
            updateInput.Token,
            updateInput.EncodingAesKey);
        
        return Task.CompletedTask;
    }
}