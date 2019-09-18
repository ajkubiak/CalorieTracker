using Lib.Models.Auth;
using Lib.Models.Diary;
using Microsoft.EntityFrameworkCore;

namespace Lib.Models.Database
{
    public class CCDbContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<DiaryEntry> DiaryEntries { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }

        public CCDbContext(DbContextOptions<CCDbContext> options)
            : base(options)
        {
            // empty, needed for creating migrations
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiaryEntry>()
                .HasIndex(item => item.OwnedById);
            modelBuilder.Entity<Meal>()
                .HasIndex(item => item.OwnedById);
            modelBuilder.Entity<FoodItem>()
                .HasIndex(item => item.OwnedById);
        }
    }
}
