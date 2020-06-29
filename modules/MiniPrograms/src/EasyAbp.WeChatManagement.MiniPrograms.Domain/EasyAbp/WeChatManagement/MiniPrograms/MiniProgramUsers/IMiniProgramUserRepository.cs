using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers
{
    public interface IMiniProgramUserRepository : IRepository<MiniProgramUser, Guid>
    {
        Task<string> FindUnionIdByOpenIdAsync(Guid miniProgramId, string openId, CancellationToken cancellationToken = default);
    }
}