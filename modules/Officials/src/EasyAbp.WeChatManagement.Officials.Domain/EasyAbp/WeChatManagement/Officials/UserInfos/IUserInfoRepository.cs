using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.WeChatManagement.Officials.UserInfos
{
    public interface IUserInfoRepository : IRepository<UserInfo, Guid>
    {
    }
}