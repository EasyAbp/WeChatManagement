using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.Common.EntityFrameworkCore
{
    [ConnectionStringName(CommonDbProperties.ConnectionStringName)]
    public interface ICommonDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<WeChatApp> WeChatApps { get; set; }
        DbSet<WeChatAppUser> WeChatAppUsers { get; set; }
    }
}