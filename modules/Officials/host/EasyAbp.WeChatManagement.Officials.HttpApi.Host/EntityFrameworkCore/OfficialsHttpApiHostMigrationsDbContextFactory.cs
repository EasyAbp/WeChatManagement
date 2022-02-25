using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EasyAbp.WeChatManagement.Officials.EntityFrameworkCore;

public class OfficialsHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<OfficialsHttpApiHostMigrationsDbContext>
{
    public OfficialsHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<OfficialsHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Officials"));

        return new OfficialsHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
