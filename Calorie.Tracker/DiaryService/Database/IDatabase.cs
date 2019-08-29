using System.Collections.Generic;
using Lib.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryService.Database
{
    public interface IDatabaseApi
    {
        DbContextOptions<T> BuildOptions<T>() where T : DbContext;
        IList<FoodItem> GetFoodItems();
        FoodItem CreateFoodItem(FoodItem foodItem);
        void DeleteFoodItem(long id);
    }
}
