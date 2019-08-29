using Lib.Models;
using Lib.Utils;
using Microsoft.EntityFrameworkCore;

namespace DiaryService.Database.EfContext
{
    public class DiaryEntryContext : DbContext
    {
        DbSet<DiaryEntry> DiaryEntries { get; set; }

        public DiaryEntryContext()
        {
        }
        public DiaryEntryContext(DbContextOptions<DiaryEntryContext> options)
            : base(options)
        {
            // empty, needed for creating migrations
        }
    }
}
