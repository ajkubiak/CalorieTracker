using System;
using Newtonsoft.Json;

namespace Lib.Models.Diary
{
    /**
     * <summary>Many-to-many relationship</summary>
     */
    public class FoodItemMeal : IManyToMany
    {
        public virtual Guid MealId { get; set; }
        [JsonIgnore]
        public virtual Meal Meal { get; set; }

        public virtual Guid FoodItemId { get; set; }
        [JsonIgnore]
        public virtual FoodItem FoodItem { get; set; }

        public void SetIdFirst(Guid id)
        {
            MealId = id;
        }

        public void SetIdSecond(Guid id)
        {
            FoodItemId = id;
        }
    }
}
