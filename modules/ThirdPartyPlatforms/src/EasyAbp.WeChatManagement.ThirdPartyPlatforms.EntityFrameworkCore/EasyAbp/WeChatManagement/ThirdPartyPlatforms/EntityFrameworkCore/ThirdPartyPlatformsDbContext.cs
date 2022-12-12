﻿using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.EntityFrameworkCore;

[ConnectionStringName(ThirdPartyPlatformsDbProperties.ConnectionStringName)]
public class ThirdPartyPlatformsDbContext : AbpDbContext<ThirdPartyPlatformsDbContext>, IThirdPartyPlatformsDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public ThirdPartyPlatformsDbContext(DbContextOptions<ThirdPartyPlatformsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureThirdPartyPlatforms();
    }
}
