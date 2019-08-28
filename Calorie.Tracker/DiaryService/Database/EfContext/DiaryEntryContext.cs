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

    /**
     * <summary>Context factory for building migrations at design time. This class
     * must exist to supply the type to the context factory</summary>
     */
    public class DiaryEntryContextFactory : BaseIDesignTimeDbContextFactory<DiaryEntryContext>
    {
        public DiaryEntryContextFactory(ISettingsUtils settingsUtils)
            : base(settingsUtils)
        {

        }
    }
}
