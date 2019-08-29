using Lib.Models;
using Lib.Utils;
using Microsoft.EntityFrameworkCore;

namespace DiaryService.Database.EfContext
{
    public class FoodItemContext : DbContext
    {
        public DbSet<FoodItem> FoodItems { get; set; }

        public FoodItemContext(DbContextOptions<FoodItemContext> options)
            : base(options)
        {
            // empty, needed for creating migrations
        }
    }
}
