using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Lib.Models.Database.Auth
{
    public class AuthDesignTimeDbContextFactory : BaseDesignTimeDbContextFactory<AuthDbContext>
    {
        protected override IConfigurationRoot getConfigurationRoot()
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
