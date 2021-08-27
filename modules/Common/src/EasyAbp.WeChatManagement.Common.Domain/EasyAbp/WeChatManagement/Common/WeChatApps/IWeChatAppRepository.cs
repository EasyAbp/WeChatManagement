using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.WeChatManagement.Common.WeChatApps
{
    public interface IWeChatAppRepository : IRepository<WeChatApp, Guid>
    {
    }
}