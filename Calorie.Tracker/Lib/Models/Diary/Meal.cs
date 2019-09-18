using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lib.Models.Auth;

namespace Lib.Models.Diary
{
    public class Meal : BaseModel
    {
        [Required]
        public string OwnedById { get; set; }

        [Required]
        [ForeignKey("OwnedById")]
        virtual public User OwnedBy { get; set; }

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
         *  The position in which this meal shows up in a grouping of meals
         * </summary>
         */
        public uint Order { get; set; }

        /**
         * <summary>
         *  A list of <see cref="FoodItem"/> objects that compose a meal
         * </summary>
         */
        [MaxLength(20)]
        public virtual ISet<FoodItem> FoodItems { get; set; }
    }
}
