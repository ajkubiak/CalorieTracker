using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace AuthService
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    // find the shared folder in the parent folder
                    var sharedFolder = Path.Combine(env.ContentRootPath, "..", "SharedConfig");

                    //load the SharedSettings first, so that appsettings.json overrwrites it
                    config
                        .AddJsonFile(Path.Combine(sharedFolder, $"appsettings.{env.EnvironmentName}.json"), false)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false);

                    config.AddEnvironmentVariables();
                })
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .WriteTo.Console());
    }
}
