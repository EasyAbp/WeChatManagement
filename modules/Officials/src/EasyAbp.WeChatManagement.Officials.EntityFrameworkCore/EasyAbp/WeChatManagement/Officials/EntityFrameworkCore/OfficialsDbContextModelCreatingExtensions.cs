using EasyAbp.WeChatManagement.Officials.UserInfos;
using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.WeChatManagement.Officials.EntityFrameworkCore
{
    public static class OfficialsDbContextModelCreatingExtensions
    {
        public static void ConfigureWeChatManagementOfficials(
            this ModelBuilder builder,
            Action<OfficialsModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new OfficialsModelBuilderConfigurationOptions(
                OfficialsDbProperties.DbTablePrefix,
                OfficialsDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */

            builder.Entity<UserInfo>(b =>
            {
                b.ToTable(options.TablePrefix + "UserInfos", options.Schema);
                b.ConfigureByConvention(); 

                /* Configure more properties here */
            });
        }
    }
}
