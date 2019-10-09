﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lib.Models.Diary
{
    public class MealDto : BaseDto
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
         *  A list of <see cref="FoodItem"/> object ids that compose a meal
         * </summary>
         */
        [MaxLength(20)]
        public virtual ISet<Guid> FoodItemIds { get; set; }
    }
}