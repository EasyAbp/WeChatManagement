using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Encryption;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;

public class AuthorizerSecret : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    [NotNull]
    public virtual string ComponentAppId { get; protected set; }

    [NotNull]
    public virtual string AuthorizerAppId { get; protected set; }

    [NotNull]
    [DisableAuditing]
    public virtual string EncryptedAccessToken { get; protected set; }

    [NotNull]
    [DisableAuditing]
    public virtual string EncryptedRefreshToken { get; protected set; }

    public virtual List<int> CategoryIds { get; protected set; }

    protected AuthorizerSecret()
    {
    }

    public AuthorizerSecret(Guid id, Guid? tenantId, [NotNull] string componentAppId, [NotNull] string authorizerAppId,
        [NotNull] string encryptedAccessToken, [NotNull] string encryptedRefreshToken, List<int> categoryIds) : base(id)
    {
        TenantId = tenantId;
        ComponentAppId = Check.NotNullOrWhiteSpace(componentAppId, nameof(componentAppId));
        AuthorizerAppId = Check.NotNullOrWhiteSpace(authorizerAppId, nameof(authorizerAppId));
        EncryptedAccessToken = Check.NotNullOrWhiteSpace(encryptedAccessToken, nameof(encryptedAccessToken));
        EncryptedRefreshToken = Check.NotNullOrWhiteSpace(encryptedRefreshToken, nameof(encryptedRefreshToken));
        CategoryIds = categoryIds ?? new List<int>();
    }

    public string GetAccessToken(IStringEncryptionService stringEncryptionService)
    {
        return stringEncryptionService.Decrypt(EncryptedAccessToken);
    }

    public void SetAccessToken([NotNull] string accessToken, IStringEncryptionService stringEncryptionService)
    {
        Check.NotNullOrWhiteSpace(accessToken, nameof(accessToken));

        EncryptedAccessToken = stringEncryptionService.Encrypt(accessToken);
    }

    public string GetRefreshToken(IStringEncryptionService stringEncryptionService)
    {
        return stringEncryptionService.Decrypt(EncryptedRefreshToken);
    }

    public void SetRefreshToken([NotNull] string refreshToken, IStringEncryptionService stringEncryptionService)
    {
        Check.NotNullOrWhiteSpace(refreshToken, nameof(refreshToken));

        EncryptedRefreshToken = stringEncryptionService.Encrypt(refreshToken);
    }
}