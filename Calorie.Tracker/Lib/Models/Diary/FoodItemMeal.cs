using System;
namespace Lib.Models.Diary
{
    /**
     * <summary>Many-to-many relationship</summary>
     */
    public class FoodItemMeal
    {
        public virtual Guid MealId { get; set; }
        public virtual Meal Meal { get; set; }

        public virtual Guid FoodItemId { get; set; }
        public virtual FoodItem FoodItem { get; set; }
    }
}
