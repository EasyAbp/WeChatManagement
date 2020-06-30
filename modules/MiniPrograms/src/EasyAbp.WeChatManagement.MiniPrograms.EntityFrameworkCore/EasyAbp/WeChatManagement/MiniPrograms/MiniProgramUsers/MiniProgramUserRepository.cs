using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers
{
    public class MiniProgramUserRepository : EfCoreRepository<MiniProgramsDbContext, MiniProgramUser, Guid>, IMiniProgramUserRepository
    {
        public MiniProgramUserRepository(IDbContextProvider<MiniProgramsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual async Task<string> FindUnionIdByOpenIdAsync(Guid miniProgramId, string openId, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(x => x.OpenId == openId && x.MiniProgramId == miniProgramId)
                .Select(x => x.UnionId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public async Task<Guid?> FindRecentlyTenantIdAsync(Guid miniProgramId, string openId, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(x => x.OpenId == openId && x.MiniProgramId == miniProgramId)
                .OrderByDescending(x => x.LastModificationTime).Select(x => x.TenantId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}