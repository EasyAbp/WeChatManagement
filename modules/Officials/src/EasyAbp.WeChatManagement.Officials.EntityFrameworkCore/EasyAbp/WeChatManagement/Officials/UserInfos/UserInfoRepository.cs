using System;
using EasyAbp.WeChatManagement.Officials.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.Officials.UserInfos
{
    public class UserInfoRepository : EfCoreRepository<IOfficialsDbContext, UserInfo, Guid>, IUserInfoRepository
    {
        public UserInfoRepository(IDbContextProvider<IOfficialsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}