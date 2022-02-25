using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.Officials.EntityFrameworkCore;

public class OfficialsHttpApiHostMigrationsDbContext : AbpDbContext<OfficialsHttpApiHostMigrationsDbContext>
{
    public OfficialsHttpApiHostMigrationsDbContext(DbContextOptions<OfficialsHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureWeChatManagementOfficials();
    }
}
