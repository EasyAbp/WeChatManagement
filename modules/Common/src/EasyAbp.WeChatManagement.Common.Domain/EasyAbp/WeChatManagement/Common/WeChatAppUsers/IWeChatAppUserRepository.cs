using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.WeChatManagement.Common.WeChatAppUsers
{
    public interface IWeChatAppUserRepository : IRepository<WeChatAppUser, Guid>
    {
        Task<string> FindUnionIdByOpenIdAsync(Guid weChatAppId, string openId, CancellationToken cancellationToken = default);
        
        Task<Guid?> FindRecentlyTenantIdAsync(string appId, string openId, bool exceptHost, CancellationToken cancellationToken = default);
        
        Task<bool> AnyInWeChatAppTypeAsync(WeChatAppType type, [NotNull] Expression<Func<WeChatAppUser, bool>> predicate);
    }
}