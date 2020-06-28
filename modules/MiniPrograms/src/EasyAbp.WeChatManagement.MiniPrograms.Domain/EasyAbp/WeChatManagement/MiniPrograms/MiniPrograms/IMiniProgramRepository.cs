using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public interface IMiniProgramRepository : IRepository<MiniProgram, Guid>
    {
    }
}