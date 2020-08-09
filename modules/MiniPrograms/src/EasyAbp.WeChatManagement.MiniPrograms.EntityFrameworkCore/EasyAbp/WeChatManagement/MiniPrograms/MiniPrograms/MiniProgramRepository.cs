using System;
using EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public class MiniProgramRepository : EfCoreRepository<IMiniProgramsDbContext, MiniProgram, Guid>, IMiniProgramRepository
    {
        public MiniProgramRepository(IDbContextProvider<IMiniProgramsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}