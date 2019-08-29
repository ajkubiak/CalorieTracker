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
}
