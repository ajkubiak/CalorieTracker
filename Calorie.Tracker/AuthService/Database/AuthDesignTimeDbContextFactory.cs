using System;
using System.IO;
using Lib.Models.Database;
using Microsoft.Extensions.Configuration;

namespace AuthService.Database
{
    public class AuthDesignTimeDbContextFactory : BaseDesignTimeDbContextFactory<AuthDbContext>
    {
        protected override IConfigurationRoot getConfigurationRoot()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");
            return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
                    .Build();
        }
    }
}
