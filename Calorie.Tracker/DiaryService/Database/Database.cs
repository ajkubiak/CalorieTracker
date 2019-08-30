using System;
using System.Collections.Generic;
using System.Linq;
using DiaryService.Database.EfContext;
using Lib.Models;
using Lib.Utils;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DiaryService.Database
{
    public class DatabaseApi : IDatabaseApi
    {
        private readonly ISettingsUtils settingsUtils;

        public DatabaseApi(ISettingsUtils settingsUtils)
        {
            this.settingsUtils = settingsUtils;
        }

        public DbContextOptions<T> BuildOptions<T>() where T : DbContext
        {
            return new DbContextOptionsBuilder<T>()
                .UseLazyLoadingProxies()
                .UseNpgsql(settingsUtils.GetDbConnectionString())
                .Options;
        }

        public FoodItem CreateFoodItem(FoodItem foodItem)
        {
            using (var context = new FoodItemContext(
                BuildOptions<FoodItemContext>()))
            {
                return context.FoodItems.Add(foodItem).Entity;
            }
        }

        /**
         * <summary>Delete a  by id</summary>
         * <exception cref="InvalidOperationException">A food item with that id does not exist</exception>
         */
        public void DeleteFoodItem(long id)
        {
            using (var context = new FoodItemContext(
                BuildOptions<FoodItemContext>()))
            {
                // data validation
                if (id == 0)
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
            using (var context = new FoodItemContext(
                BuildOptions<FoodItemContext>()))
            {
                return context.FoodItems.ToList();
            }
        }
    }
}
