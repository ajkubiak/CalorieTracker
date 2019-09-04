using System;
using System.Collections.Generic;
using Lib.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryService.Database
{
    public interface IDatabaseApi
    {
        IList<FoodItem> GetFoodItems();
        FoodItem CreateFoodItem(FoodItem foodItem);
        void DeleteFoodItem(Guid id);
    }
}
