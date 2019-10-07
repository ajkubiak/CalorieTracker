using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Lib.Models.Diary
{
    public class Meal : BaseModel, IManyToMany<FoodItemMeal>
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
        public virtual ISet<FoodItemMeal> ManyToMany { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Meal() { }

        public Meal(MealDto mealDto)
        {
            Name = mealDto.Name;
            Order = mealDto.Order;
        }
    }
}
