using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers
{
    public interface IMiniProgramUserRepository : IRepository<MiniProgramUser, Guid>
    {
    }
}