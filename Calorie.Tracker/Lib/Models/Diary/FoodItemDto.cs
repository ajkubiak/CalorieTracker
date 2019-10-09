using Lib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lib.Models.Diary
{
    /**
     * <summary>Transport for <see cref="FoodItem"/></summary>
     */
    public class FoodItemDto : BaseDto
    { 
        /**
         * <summary>
         *  The name of this food
         * </summary>
         */
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /**
         * <summary>
         *  The amount of carbohydrates in this food in grams
         * </summary>
         */
        [Required]
        public float Carbohydrates { get; set; }

        /**
         * <summary>
         *  The amount of protein in this food in grams
         * </summary>
         */
        [Required]
        public float Protein { get; set; }

        /**
         * <summary>
         *  The amount of fat in this food in grams
         * </summary>
         */
        [Required]
        public float Fat { get; set; }

        /**
         * <summary>
         *  A list of <see cref="Meal"/> object ids in which this food is included
         * </summary>
         */
        public virtual ISet<Guid> MealIds { get; set; }
    }
}
