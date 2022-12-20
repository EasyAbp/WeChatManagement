using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Uow;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms;

public class ThirdPartyPlatformsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly IWeChatAppRepository _weChatAppRepository;
    private readonly IAuthorizerSecretRepository _authorizerSecretRepository;
    private readonly IStringEncryptionService _stringEncryptionService;

    public ThirdPartyPlatformsDataSeedContributor(
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IWeChatAppRepository weChatAppRepository,
        IAuthorizerSecretRepository authorizerSecretRepository,
        IStringEncryptionService stringEncryptionService)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _weChatAppRepository = weChatAppRepository;
        _authorizerSecretRepository = authorizerSecretRepository;
        _stringEncryptionService = stringEncryptionService;
    }

    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        /* Instead of returning the Task.CompletedTask, you can insert your test data
         * at this point!
         */

        using var changeTenant = _currentTenant.Change(context?.TenantId);

        await SeedThirdPartyPlatformWeChatAppsAsync(context);
        await SeedAuthorizerSecretsAsync(context);
    }

    private async Task SeedThirdPartyPlatformWeChatAppsAsync(DataSeedContext context)
    {
        await _weChatAppRepository.InsertAsync(new WeChatApp(
            _guidGenerator.Create(),
            _currentTenant.Id,
            WeChatAppType.ThirdPartyPlatform,
            null,
            "my-3rd-platform",
            "我的第三方平台",
            "Default",
            ThirdPartyPlatformsTestConsts.AppId,
            _stringEncryptionService.Encrypt(ThirdPartyPlatformsTestConsts.AppSecret),
            _stringEncryptionService.Encrypt(ThirdPartyPlatformsTestConsts.Token),
            _stringEncryptionService.Encrypt(ThirdPartyPlatformsTestConsts.EncodingAesKey),
            false
        ), true);
    }

    private async Task SeedAuthorizerSecretsAsync(DataSeedContext context)
    {
        var encryptedRefreshToken =
            _stringEncryptionService.Encrypt(ThirdPartyPlatformsTestConsts.AuthorizerRefreshToken);

        await _authorizerSecretRepository.InsertAsync(new AuthorizerSecret(_guidGenerator.Create(), _currentTenant.Id,
            ThirdPartyPlatformsTestConsts.AppId, ThirdPartyPlatformsTestConsts.AuthorizerAppId, encryptedRefreshToken,
            new List<int> { 1, 2, 3 }), true);
    }
}