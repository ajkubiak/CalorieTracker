using System;
using Lib.Utils;
using Microsoft.EntityFrameworkCore;

namespace Lib.Models
{
    public abstract class BaseDbContext : DbContext
    {
        protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder, ISettingsUtils settingsUtils)
        {
            optionsBuilder.UseNpgsql(settingsUtils.GetDbConnectionString());
        }
    }
}
