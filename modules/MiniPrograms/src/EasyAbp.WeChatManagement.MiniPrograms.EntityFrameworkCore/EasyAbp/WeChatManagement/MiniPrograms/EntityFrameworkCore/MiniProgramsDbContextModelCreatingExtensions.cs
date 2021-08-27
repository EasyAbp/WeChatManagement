using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore
{
    public static class MiniProgramsDbContextModelCreatingExtensions
    {
        public static void ConfigureWeChatManagementMiniPrograms(
            this ModelBuilder builder,
            Action<MiniProgramsModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new MiniProgramsModelBuilderConfigurationOptions(
                MiniProgramsDbProperties.DbTablePrefix,
                MiniProgramsDbProperties.DbSchema
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
