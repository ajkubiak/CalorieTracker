using System.Collections.Generic;
using Lib.Models.Diary;

namespace Lib.Models.Database.Diary
{
    public interface IDiaryDb : ICrudOps
    {
        #region FoodItems
        //FoodItem GetFoodItem(Guid id);
        IList<FoodItem> GetFoodItems();
        //FoodItem CreateFoodItem(FoodItem foodItem);
        //void DeleteFoodItem(Guid id);
        #endregion

        #region Meals
        //Meal CreateMeal(Meal meal);
        #endregion
    }
}
