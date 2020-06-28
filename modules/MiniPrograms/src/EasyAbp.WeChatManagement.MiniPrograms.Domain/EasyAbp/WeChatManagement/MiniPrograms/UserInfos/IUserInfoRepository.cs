using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public interface IUserInfoRepository : IRepository<UserInfo, Guid>
    {
    }
}