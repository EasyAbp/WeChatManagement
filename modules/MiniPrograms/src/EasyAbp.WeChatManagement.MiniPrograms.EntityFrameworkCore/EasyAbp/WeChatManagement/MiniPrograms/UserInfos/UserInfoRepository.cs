using System;
using EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public class UserInfoRepository : EfCoreRepository<IMiniProgramsDbContext, UserInfo, Guid>, IUserInfoRepository
    {
        public UserInfoRepository(IDbContextProvider<IMiniProgramsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}