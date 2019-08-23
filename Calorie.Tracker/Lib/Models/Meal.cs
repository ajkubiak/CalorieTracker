using System;
using System.Collections.Generic;
using Lib.Utils;
using Microsoft.EntityFrameworkCore;

namespace Lib.Models
{

    public class Meal : BaseModel
    {
        public IEnumerable<FoodItem> FoodItems { get; set; }
    }

    public class MealContext : BaseDbContext
    {
        DbSet<FoodItem> Meals { get; set; }
    }
}
