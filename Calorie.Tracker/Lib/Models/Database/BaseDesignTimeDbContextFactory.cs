using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Lib.Models.Database
{
    /**
     * <summary>Context factory for building migrations at design time</summary>
     */
    public abstract class BaseDesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        protected abstract IConfigurationRoot getConfigurationRoot();

        public T CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = getConfigurationRoot();
            var host = configuration.GetValue<string>("DB_HOST");
            var databaseName = configuration.GetValue<string>("DB_NAME");
            var username = configuration.GetValue<string>("DB_USER");
            var password = configuration.GetValue<string>("DB_PASS");
            var dbstring = $"Host={host};Database={databaseName};Username={username};Password={password}";

            Console.WriteLine(dbstring);

            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseNpgsql(dbstring);
            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);
        }
    }
}
