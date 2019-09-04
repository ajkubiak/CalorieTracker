using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long Id { get; set; }

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

        public FoodItem() { }

        public FoodItem(float carbohydrates, float protein, float fat)
        {
            Carbohydrates = carbohydrates;
            Protein = protein;
            Fat = fat;
        }
    }
}
