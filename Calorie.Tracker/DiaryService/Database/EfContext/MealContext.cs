using Lib.Models;
using Lib.Utils;
using Microsoft.EntityFrameworkCore;

namespace DiaryService.Database.EfContext
{
    public class MealContext : DbContext
    {
        DbSet<FoodItem> Meals { get; set; }

        public MealContext()
        {
        }
        public MealContext(DbContextOptions<MealContext> options)
            : base(options)
        {
            // empty, needed for creating migrations
        }
    }

    /**
     * <summary>Context factory for building migrations at design time. This class
     * must exist to supply the type to the context factory</summary>
     */
    public class MealContextFactory : BaseIDesignTimeDbContextFactory<MealContext>
    {
        public MealContextFactory(ISettingsUtils settingsUtils)
            : base(settingsUtils)
        {

        }
    }
}
