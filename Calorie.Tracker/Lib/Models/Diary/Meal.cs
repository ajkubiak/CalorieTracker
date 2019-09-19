﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Lib.Models.Diary
{
    public class Meal : BaseModel
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
         *  A list of <see cref="FoodItem"/> objects that compose a meal
         * </summary>
         */
        [MaxLength(20)]
        public virtual ISet<FoodItem> FoodItems { get; set; }

        public Meal() { }

        public Meal(MealDto mealDto)
        {
            Name = mealDto.Name;
            Order = mealDto.Order;
            FoodItems = mealDto.FoodItems;
        }
    }
}
