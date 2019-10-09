using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Lib.Models.Diary
{
    public class Meal : BaseModel, IDtoGenerator<MealDto>
    {
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
         *  A many-to-many relationship with <see cref="FoodItem"/>
         * </summary>
         */
        [MaxLength(20)]
        public virtual ICollection<FoodItemMeal> FoodItemLinks { get; set; }

        public MealDto GenerateDto()
        {
            ISet<Guid> foodItemIds = new HashSet<Guid>();
            foreach (FoodItemMeal foodItemMeal in FoodItemLinks)
            {
                foodItemIds.Add(foodItemMeal.FoodItemId);
            }
            return new MealDto()
            {
                Id = this.Id,
                Name = this.Name,
                Order = this.Order,
                FoodItemIds = foodItemIds
            };
        }
    }
}
