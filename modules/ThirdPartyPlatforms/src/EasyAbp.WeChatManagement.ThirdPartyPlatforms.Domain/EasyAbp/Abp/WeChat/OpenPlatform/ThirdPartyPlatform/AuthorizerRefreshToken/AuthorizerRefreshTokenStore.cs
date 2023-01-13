using System.Threading.Tasks;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Uow;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AuthorizerRefreshToken;

/// <summary>
/// 本类替换了 CacheAuthorizerRefreshTokenStore 的实现，将信息持久化。
/// </summary>
[UnitOfWork]
public class AuthorizerRefreshTokenStore : IAuthorizerRefreshTokenStore, ITransientDependency
{
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IAuthorizerSecretRepository _authorizerSecretRepository;
    private readonly CacheAuthorizerRefreshTokenStore _cache;

    public AuthorizerRefreshTokenStore(
        IStringEncryptionService stringEncryptionService,
        IAuthorizerSecretRepository authorizerSecretRepository,
        CacheAuthorizerRefreshTokenStore cache)
    {
        _stringEncryptionService = stringEncryptionService;
        _authorizerSecretRepository = authorizerSecretRepository;
        _cache = cache;
    }

    public virtual async Task<string> GetOrNullAsync(string componentAppId, string authorizerAppId)
    {
        var cachedValue = await _cache.GetOrNullAsync(componentAppId, authorizerAppId);

        if (cachedValue != null)
        {
            return cachedValue;
        }

        var authorizerSecret = await _authorizerSecretRepository.FindAsync(x =>
            x.ComponentAppId == componentAppId && x.AuthorizerAppId == authorizerAppId);

        cachedValue = authorizerSecret?.GetRefreshToken(_stringEncryptionService);

        await _cache.SetAsync(componentAppId, authorizerAppId, cachedValue);

        return cachedValue;
    }

    public virtual async Task SetAsync(string componentAppId, string authorizerAppId, string authorizerRefreshToken)
    {
        // Set only the cache value.
        await _cache.SetAsync(componentAppId, authorizerAppId, authorizerRefreshToken);
    }
}