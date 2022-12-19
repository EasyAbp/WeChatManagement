using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Uow;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.VerifyTicket;

/// <summary>
/// 本类替换了 CacheComponentVerifyTicketStore 的实现，将信息持久化。
/// </summary>
public class ComponentVerifyTicketStore : IComponentVerifyTicketStore, ITransientDependency
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IStringEncryptionService _stringEncryptionService;
    private readonly IWeChatAppRepository _weChatAppRepository;
    private readonly IDistributedCache<string> _cache;

    public ComponentVerifyTicketStore(
        IUnitOfWorkManager unitOfWorkManager,
        IStringEncryptionService stringEncryptionService,
        IWeChatAppRepository weChatAppRepository,
        IDistributedCache<string> cache)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _stringEncryptionService = stringEncryptionService;
        _weChatAppRepository = weChatAppRepository;
        _cache = cache;
    }

    [UnitOfWork]
    public virtual async Task<string> GetOrNullAsync(string componentAppId)
    {
        var cacheKey = await GetCacheKeyAsync(componentAppId);
        var cache = await _cache.GetAsync(cacheKey);

        if (cache != null)
        {
            return cache;
        }

        var weChatApp = await _weChatAppRepository.FindThirdPartyPlatformAppByAppIdAsync(componentAppId);

        cache = weChatApp?.GetVerifyTicketOrNullAsync(_stringEncryptionService);

        await _cache.SetAsync(cacheKey, cache);

        return cache;
    }

    public virtual async Task SetAsync(string componentAppId, string componentVerifyTicket)
    {
        using var uow = _unitOfWorkManager.Begin(true);

        var weChatApp = await _weChatAppRepository.GetThirdPartyPlatformAppByAppIdAsync(componentAppId);

        weChatApp.SetVerifyTicketAsync(componentVerifyTicket, _stringEncryptionService);

        await _weChatAppRepository.UpdateAsync(weChatApp, true);

        await uow.CompleteAsync();

        await _cache.SetAsync(await GetCacheKeyAsync(componentAppId), componentVerifyTicket,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(715)
            });
    }

    protected virtual async Task<string> GetCacheKeyAsync(string componentAppId) =>
        $"WeChatComponentVerifyTicket:{componentAppId}";
}