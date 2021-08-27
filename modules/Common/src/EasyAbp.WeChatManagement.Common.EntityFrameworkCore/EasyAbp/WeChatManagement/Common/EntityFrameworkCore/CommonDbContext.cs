using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.Common.EntityFrameworkCore
{
    [ConnectionStringName(CommonDbProperties.ConnectionStringName)]
    public class CommonDbContext : AbpDbContext<CommonDbContext>, ICommonDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public DbSet<WeChatApp> WeChatApps { get; set; }
        public DbSet<WeChatAppUser> WeChatAppUsers { get; set; }
        
        public CommonDbContext(DbContextOptions<CommonDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureWeChatManagementCommon();
        }
    }
}