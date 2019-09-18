using System;
using System.Collections.Generic;
using System.Linq;
using Lib.Models.Diary;
using Lib.Utils;
using Microsoft.AspNetCore.Http;

namespace Lib.Models.Database.Diary
{
    public class DiaryDb : BaseDatabaseApi, IDiaryDb
    {
        public DiaryDb(IHttpContextAccessor httpContextAccessor, ISettingsUtils settingsUtils)
            : base(httpContextAccessor, settingsUtils)
        {
            // empty
        }
        

        public FoodItem CreateFoodItem(FoodItem foodItem)
        {
            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                return context.FoodItems.Add(foodItem).Entity;
            }
        }

        /**
         * <summary>Delete a <see cref="FoodItem"/> by id</summary>
         * <exception cref="InvalidOperationException">A food item with that id does not exist</exception>
         */
        public void DeleteFoodItem(Guid id)
        {
            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                // data validation
                if (id == Guid.Empty)
                {
                    throw new ArgumentException("There was no id provided to this method");
                }

                FoodItem food = null;
                //food = context.FoodItems.First(x => x.Id == id);
                context.FoodItems.Remove(food);
            }
        }

        public IList<FoodItem> GetFoodItems()
        {
            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                return context.FoodItems.ToList();
            }
        }
    }
}
