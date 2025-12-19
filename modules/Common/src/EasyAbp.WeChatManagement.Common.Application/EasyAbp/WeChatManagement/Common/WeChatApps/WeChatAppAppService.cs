using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.Localization;
using EasyAbp.WeChatManagement.Common.Permissions;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Security.Encryption;

namespace EasyAbp.WeChatManagement.Common.WeChatApps;

public class WeChatAppAppService : CrudAppService<WeChatApp, WeChatAppDto, Guid, WeChatAppGetListInput,
    CreateWeChatAppDto, UpdateWeChatAppDto>, IWeChatAppAppService
{
    protected override string GetPolicyName { get; set; } = CommonPermissions.WeChatApp.Default;
    protected override string GetListPolicyName { get; set; } = CommonPermissions.WeChatApp.Default;
    protected override string CreatePolicyName { get; set; } = CommonPermissions.WeChatApp.Create;
    protected override string UpdatePolicyName { get; set; } = CommonPermissions.WeChatApp.Update;
    protected override string DeletePolicyName { get; set; } = CommonPermissions.WeChatApp.Delete;

    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IWeChatAppRepository _repository;

    public WeChatAppAppService(
        IStringEncryptionService stringEncryptionService,
        IWeChatAppRepository repository) : base(repository)
    {
        _stringEncryptionService = stringEncryptionService;
        _repository = repository;

        LocalizationResource = typeof(CommonResource);
        ObjectMapperContext = typeof(WeChatManagementCommonApplicationModule);
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

    protected override async Task<WeChatAppDto> MapToGetOutputDtoAsync(WeChatApp entity)
    {
        var dto = await base.MapToGetOutputDtoAsync(entity);

        dto.AppSecret = _stringEncryptionService.Decrypt(entity.EncryptedAppSecret);
        dto.Token = _stringEncryptionService.Decrypt(entity.EncryptedToken);
        dto.EncodingAesKey = _stringEncryptionService.Decrypt(entity.EncryptedEncodingAesKey);

        return dto;
    }

    protected override Task<WeChatApp> MapToEntityAsync(CreateWeChatAppDto createInput)
    {
        return Task.FromResult(new WeChatApp(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            createInput.Type,
            createInput.ComponentWeChatAppId,
            createInput.Name,
            createInput.DisplayName,
            createInput.OpenAppIdOrName,
            createInput.AppId,
            _stringEncryptionService.Encrypt(createInput.AppSecret),
            _stringEncryptionService.Encrypt(createInput.AppSecret),
            _stringEncryptionService.Encrypt(createInput.EncodingAesKey),
            false));
    }

    protected override Task MapToEntityAsync(UpdateWeChatAppDto updateInput, WeChatApp entity)
    {
        entity.Update(
            updateInput.ComponentWeChatAppId,
            updateInput.Name,
            updateInput.DisplayName,
            updateInput.OpenAppIdOrName,
            updateInput.AppId,
            _stringEncryptionService.Encrypt(updateInput.AppSecret),
            _stringEncryptionService.Encrypt(updateInput.Token),
            _stringEncryptionService.Encrypt(updateInput.EncodingAesKey));

        return Task.CompletedTask;
    }
}