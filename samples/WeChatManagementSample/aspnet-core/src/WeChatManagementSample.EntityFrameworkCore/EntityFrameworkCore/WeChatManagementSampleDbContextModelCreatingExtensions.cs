using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace WeChatManagementSample.EntityFrameworkCore
{
    public static class WeChatManagementSampleDbContextModelCreatingExtensions
    {
        public static void ConfigureWeChatManagementSample(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(WeChatManagementSampleConsts.DbTablePrefix + "YourEntities", WeChatManagementSampleConsts.DbSchema);

            //    //...
            //});
        }
    }
}