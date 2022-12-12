using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EntityFrameworkCore;

public static class ThirdPartyPlatformsDbContextModelCreatingExtensions
{
    public static void ConfigureThirdPartyPlatforms(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(ThirdPartyPlatformsDbProperties.DbTablePrefix + "Questions", ThirdPartyPlatformsDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
    }
}
