using System;
using Lib.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DiaryService.Database.EfContext
{
    /**
     * <summary>Context factory for building migrations at design time</summary>
     */
    public abstract class BaseIDesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        private const string DesignTimeDbString = "Host=localhost;Database=migrations;Username=test;Password=savasana";
        protected readonly ISettingsUtils _settingsUtils;
        protected BaseIDesignTimeDbContextFactory(ISettingsUtils settingsUtils)
        {
            _settingsUtils = settingsUtils;
        }

        public T CreateDbContext(string[] args)
        {
            //string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //IConfiguration config = new ConfigurationBuilder()
            //        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EfDesignDemo.Web"))
            //        //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //        .AddJsonFile($"appsettings.{environment}.json", optional: true)
            //        .AddEnvironmentVariables()
            //        .Build();
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseNpgsql(_settingsUtils.GetDbConnectionString());
            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);
        }

        public DbContextOptions<T> BuildOptions<T>() where T : DbContext
        {
            return new DbContextOptionsBuilder<T>()
                .UseLazyLoadingProxies()
                .UseNpgsql(GetDbConnectionString())
                .Options;
        }
    }
}
