using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DiaryService.Database.EfContext
{
    /**
     * <summary>Context factory for building migrations at design time</summary>
     */
    public abstract class BaseIDesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        public T CreateDbContext(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
                    .Build();

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

    public class DiaryEntryDesignTimeContextFactory : BaseIDesignTimeDbContextFactory<DiaryEntryContext>
    {
        // empty
    }

    public class MealDesignTimeContextFactory : BaseIDesignTimeDbContextFactory<MealContext>
    {
        // empty
    }

    public class FoodItemDesignTimeContextFactory : BaseIDesignTimeDbContextFactory<FoodItemContext>
    {
        // empty
    }
}
