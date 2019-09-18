using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Lib.Models.Database
{
    public class CCDesignTimeDbContext : IDesignTimeDbContextFactory<CCDbContext>
    {
        public CCDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = GetConfigurationRoot();
            var host = configuration.GetValue<string>("DB_HOST");
            var databaseName = configuration.GetValue<string>("DB_NAME");
            var username = configuration.GetValue<string>("DB_USER");
            var password = configuration.GetValue<string>("DB_PASS");
            var dbstring = $"Host={host};Database={databaseName};Username={username};Password={password}";

            Console.WriteLine(dbstring);

            var optionsBuilder = new DbContextOptionsBuilder<CCDbContext>();
            optionsBuilder.UseNpgsql(dbstring);
            return new CCDbContext(optionsBuilder.Options);
        }

        private IConfigurationRoot GetConfigurationRoot()
        {
            // find the shared folder in the parent folder
            var sharedFolder = Path.Combine(Directory.GetCurrentDirectory(), "..", "SharedConfig");

            // Default to Development appsettings
            return new ConfigurationBuilder()
                    .SetBasePath(sharedFolder)
                    .AddJsonFile("appsettings.Development.json")
                    .Build();
        }
    }
}
