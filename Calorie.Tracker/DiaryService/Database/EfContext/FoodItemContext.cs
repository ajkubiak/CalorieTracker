using Lib.Models;
using Lib.Utils;
using Microsoft.EntityFrameworkCore;

namespace DiaryService.Database.EfContext
{
    public class FoodItemContext : DbContext
    {
        public DbSet<FoodItem> FoodItems { get; set; }

        public FoodItemContext() { }
        public FoodItemContext(DbContextOptions<FoodItemContext> options)
            : base(options)
        {
            // empty, needed for creating migrations
        }
    }

    /**
     * <summary>Context factory for building migrations at design time. This class
     * must exist to supply the type to the context factory</summary>
     */
    public class FoodItemContextFactory : BaseIDesignTimeDbContextFactory<FoodItemContext>
    {
        public FoodItemContextFactory(ISettingsUtils settingsUtils)
            : base(settingsUtils)
        {

        }
    }
}
