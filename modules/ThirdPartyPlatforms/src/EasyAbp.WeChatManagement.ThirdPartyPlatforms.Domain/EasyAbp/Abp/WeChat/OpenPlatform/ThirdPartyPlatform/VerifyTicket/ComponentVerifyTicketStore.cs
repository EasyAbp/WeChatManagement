using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
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
    private readonly CacheComponentVerifyTicketStore _cache;

    public ComponentVerifyTicketStore(
        IUnitOfWorkManager unitOfWorkManager,
        IStringEncryptionService stringEncryptionService,
        IWeChatAppRepository weChatAppRepository,
        CacheComponentVerifyTicketStore cache)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _stringEncryptionService = stringEncryptionService;
        _weChatAppRepository = weChatAppRepository;
        _cache = cache;
    }

    [UnitOfWork]
    public virtual async Task<string> GetOrNullAsync(string componentAppId)
    {
        var cachedValue = await _cache.GetOrNullAsync(componentAppId);

        if (cachedValue != null)
        {
            return cachedValue;
        }

        var weChatApp = await _weChatAppRepository.FindThirdPartyPlatformAppByAppIdAsync(componentAppId);

        cachedValue = weChatApp?.GetVerifyTicketOrNullAsync(_stringEncryptionService);

        await _cache.SetAsync(componentAppId, cachedValue);

        return cachedValue;
    }

    public virtual async Task SetAsync(string componentAppId, string componentVerifyTicket)
    {
        using var uow = _unitOfWorkManager.Begin(true);

        var weChatApp = await _weChatAppRepository.GetThirdPartyPlatformAppByAppIdAsync(componentAppId);

        weChatApp.SetVerifyTicketAsync(componentVerifyTicket, _stringEncryptionService);

        await _weChatAppRepository.UpdateAsync(weChatApp, true);

        await uow.CompleteAsync();

        await _cache.SetAsync(componentAppId, componentVerifyTicket);
    }
}