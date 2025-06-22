using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace EasyAbp.WeChatManagement.MiniPrograms.Identity
{
    public interface IUniquePhoneNumberIdentityUserRepository : IRepository<IdentityUser, Guid>, IIdentityUserRepository
    {
        Task<IdentityUser> FindByConfirmedPhoneNumberAsync([NotNull] string phoneNumber, bool includeDetails = true, CancellationToken cancellationToken = default);
        
        Task<IdentityUser> GetByConfirmedPhoneNumberAsync([NotNull] string phoneNumber, bool includeDetails = true, CancellationToken cancellationToken = default);
    }
}