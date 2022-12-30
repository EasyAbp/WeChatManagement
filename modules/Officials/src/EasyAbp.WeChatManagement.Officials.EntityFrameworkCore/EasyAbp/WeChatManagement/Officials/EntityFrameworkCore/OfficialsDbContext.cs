using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.WeChatManagement.Officials.UserInfos;

namespace EasyAbp.WeChatManagement.Officials.EntityFrameworkCore
{
    [ConnectionStringName(OfficialsDbProperties.ConnectionStringName)]
    public class OfficialsDbContext : AbpDbContext<OfficialsDbContext>, IOfficialsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public DbSet<UserInfo> UserInfos { get; set; }

        public OfficialsDbContext(DbContextOptions<OfficialsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureWeChatManagementOfficials();
        }
    }
}
