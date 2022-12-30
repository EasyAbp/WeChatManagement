using EasyAbp.WeChatManagement.Officials.UserInfos;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.Officials.EntityFrameworkCore
{
    [ConnectionStringName(OfficialsDbProperties.ConnectionStringName)]
    public interface IOfficialsDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<UserInfo> UserInfos { get; set; }
    }
}
