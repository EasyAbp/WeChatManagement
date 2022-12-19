using EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EntityFrameworkCore;

public static class ThirdPartyPlatformsDbContextModelCreatingExtensions
{
    public static void ConfigureWeChatManagementThirdPartyPlatforms(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<AuthorizerSecret>(b =>
        {
            b.ToTable(ThirdPartyPlatformsDbProperties.DbTablePrefix + "AuthorizerSecrets",
                ThirdPartyPlatformsDbProperties.DbSchema);
            b.ConfigureByConvention();

            /* Configure more properties here */
            b.HasIndex(x => new { x.ComponentAppId, x.AuthorizerAppId });
            b.Property(x => x.CategoryIds)
                .HasConversion<CategoryIdsListToStringValueConverter>()
                .Metadata.SetValueComparer(new CategoryIdsListValueComparer());
        });
    }
}