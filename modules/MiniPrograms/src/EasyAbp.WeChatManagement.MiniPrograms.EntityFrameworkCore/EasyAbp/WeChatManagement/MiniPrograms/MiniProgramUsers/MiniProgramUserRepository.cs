using System;
using EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers
{
    public class MiniProgramUserRepository : EfCoreRepository<MiniProgramsDbContext, MiniProgramUser, Guid>, IMiniProgramUserRepository
    {
        public MiniProgramUserRepository(IDbContextProvider<MiniProgramsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}