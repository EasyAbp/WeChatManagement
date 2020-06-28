using System;
using EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public class UserInfoRepository : EfCoreRepository<MiniProgramsDbContext, UserInfo, Guid>, IUserInfoRepository
    {
        public UserInfoRepository(IDbContextProvider<MiniProgramsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}