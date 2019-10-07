using System.Collections.Generic;
using Lib.Models.Diary;

namespace Lib.Models.Database.Diary
{
    public interface IDiaryDb : ICrudOps
    {
        #region FoodItems
        IList<FoodItem> GetFoodItems();
        #endregion

        #region Meals
        //Meal CreateMeal(Meal meal);
        #endregion
    }
}
