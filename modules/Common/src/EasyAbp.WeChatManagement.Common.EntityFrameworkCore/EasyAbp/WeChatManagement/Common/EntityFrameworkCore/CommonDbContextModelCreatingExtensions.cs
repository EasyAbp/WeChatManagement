using System;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.WeChatManagement.Common.EntityFrameworkCore
{
    public static class CommonDbContextModelCreatingExtensions
    {
        public static void ConfigureWeChatManagementCommon(
            this ModelBuilder builder,
            Action<CommonModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new CommonModelBuilderConfigurationOptions(
                CommonDbProperties.DbTablePrefix,
                CommonDbProperties.DbSchema
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
            
            builder.Entity<WeChatApp>(b =>
            {
                b.ToTable(options.TablePrefix + "WeChatApps", options.Schema);
                b.ConfigureByConvention(); 

                /* Configure more properties here */
            });


            builder.Entity<WeChatAppUser>(b =>
            {
                b.ToTable(options.TablePrefix + "WeChatAppUsers", options.Schema);
                b.ConfigureByConvention(); 

                /* Configure more properties here */
            });
        }
    }
}