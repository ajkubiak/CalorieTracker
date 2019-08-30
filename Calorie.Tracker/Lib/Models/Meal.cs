using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib.Models
{
    public class Meal : BaseModel
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long Id { get; set; }

        /**
         * <summary>
         *  The name of this meal, e.g. Breakfast, Lunch, Dinner
         * </summary>
         */
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /**
         * <summary>
         *  A list of <see cref="FoodItem"/> objects that compose a meal
         * </summary>
         */
        public virtual ISet<FoodItem> FoodItems { get; set; }
    }
}
