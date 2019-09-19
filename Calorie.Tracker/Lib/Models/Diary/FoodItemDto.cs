using System;
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

        public FoodItemDto() { }
        public FoodItemDto(FoodItem foodItem)
        {
            Name = foodItem.Name;
            Carbohydrates = foodItem.Carbohydrates;
            Protein = foodItem.Protein;
            Fat = foodItem.Fat;
        }
    }
}
