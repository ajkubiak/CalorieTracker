using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lib.Models
{
    public class DiaryEntry : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime EntryDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }
        /**
         * <summary>
         * A daily grouping of meals which are grouped by a meal "title" such as
         * "Breakfast"
         * </summary>
         */
        public IDictionary<string, Meal> MealGroupings { get; set; }
    }

    public class DiaryEntryContext : BaseDbContext
    {
        DbSet<DiaryEntry> DiaryEntries { get; set; }
    }
}
