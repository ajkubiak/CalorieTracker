using System;
using Lib.Utils;
using Microsoft.EntityFrameworkCore;

namespace Lib.Models.Database
{
    public abstract class BaseDatabaseApi
    {
        protected readonly ISettingsUtils settingsUtils;

        protected BaseDatabaseApi(ISettingsUtils settingsUtils)
        {
            this.settingsUtils = settingsUtils;
        }

        protected DbContextOptions<T> BuildOptions<T>() where T : DbContext
        {
            return new DbContextOptionsBuilder<T>()
                .UseLazyLoadingProxies()
                .UseNpgsql(settingsUtils.GetDbConnectionString())
                .Options;
        }
    }
}
