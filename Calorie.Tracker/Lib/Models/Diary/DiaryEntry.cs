using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib.Models.Diary
{
    public class DiaryEntry : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime EntryDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }

        /**
         * <summary>
         *  A daily grouping of meals which are grouped by a meal "title" such as
         *  "Breakfast". The meals are ordered by the key and has a maximum of 10
         *  groupings.
         * </summary>
         */
         [MaxLength(10)]
        public virtual ISet<Meal> MealGroupings { get; set; }
    }
}
