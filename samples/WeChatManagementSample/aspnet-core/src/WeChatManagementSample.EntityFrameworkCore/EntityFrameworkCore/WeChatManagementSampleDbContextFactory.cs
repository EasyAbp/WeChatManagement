using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WeChatManagementSample.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class WeChatManagementSampleDbContextFactory : IDesignTimeDbContextFactory<WeChatManagementSampleDbContext>
    {
        public WeChatManagementSampleDbContext CreateDbContext(string[] args)
        {
            WeChatManagementSampleEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<WeChatManagementSampleDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new WeChatManagementSampleDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WeChatManagementSample.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
