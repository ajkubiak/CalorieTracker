using System;
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
    public class FoodItem : BaseModel, IDtoGenerator<FoodItemDto>
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
        public virtual ICollection<FoodItemMeal> MealLinks { get; set; }

        public FoodItemDto GenerateDto()
        {
            ISet<Guid> mealIds = new HashSet<Guid>();
            foreach (FoodItemMeal foodItemMeal in MealLinks)
            {
                mealIds.Add(foodItemMeal.MealId);
            }
            return new FoodItemDto()
            {
                Id = this.Id,
                Name = this.Name,
                Carbohydrates = this.Carbohydrates,
                Protein = this.Protein,
                Fat = this.Fat,
                MealIds = mealIds
            };
        }
    }
}
