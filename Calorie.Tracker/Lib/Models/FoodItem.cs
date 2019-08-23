using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Lib.Models
{
    public class FoodItem : BaseModel
    {
        [Required]
        public float Carbohydrates { get; set; }
        [Required]
        public float Protein { get; set; }
        [Required]
        public float Fat { get; set; }

        public FoodItem()
        {
        }

        public FoodItem(float carbohydrates, float protein, float fat)
        {
            Carbohydrates = carbohydrates;
            Protein = protein;
            Fat = fat;
        }
    }

    public class FoodItemContext : BaseDbContext
    {
        DbSet<FoodItem> FoodItems { get; set; }
    }
}
