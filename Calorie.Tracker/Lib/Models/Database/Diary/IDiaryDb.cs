using System;
using System.Collections.Generic;
using Lib.Models.Diary;

namespace Lib.Models.Database.Diary
{
    public interface IDiaryDb
    {
        IList<FoodItem> GetFoodItems();
        FoodItem CreateFoodItem(FoodItem foodItem);
        void DeleteFoodItem(Guid id);
    }
}
