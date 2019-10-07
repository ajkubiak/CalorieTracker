using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lib.Models.Auth;
using Newtonsoft.Json;

namespace Lib.Models.Diary
{
    /**
     * <summary>
     *  A line item of food, e.g. Apple, Lettuce, Chocolate bar.
     * </summary>
     * 
     */
    public class FoodItem : BaseModel
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
         *  A many-to-many relationship with <see cref="Meal"/>
         * </summary>
         */
        [MaxLength(20)]
        public virtual ISet<FoodItemMeal> MealLinks { get; set; }

        public FoodItem() { }

        public FoodItem(float carbohydrates, float protein, float fat)
        {
            Carbohydrates = carbohydrates;
            Protein = protein;
            Fat = fat;
        }

        public FoodItem(FoodItemDto foodDto)
        {
            Name = foodDto.Name;
            Carbohydrates = foodDto.Carbohydrates;
            Protein = foodDto.Protein;
            Fat = foodDto.Fat;
        }
    }
}
