using System.Collections.Generic;
using System.Linq;
using Lib.Models.Diary;
using Lib.Utils;
using Microsoft.AspNetCore.Http;

namespace Lib.Models.Database.Diary
{
    public class DiaryDb : BaseDatabaseApi, IDiaryDb
    {
        public DiaryDb(IHttpContextAccessor httpContextAccessor, ISettingsUtils settingsUtils, IAuthUtils authUtils)
            : base(httpContextAccessor, settingsUtils, authUtils)
        {
            // empty
        }

        #region Bulk Operations
        public IList<FoodItem> GetFoodItems()
        {
            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                return context.FoodItems.ToList();
            }
        }
        #endregion

        #region Single Object Operations
        #region Food Items
        //public FoodItem CreateFoodItem(FoodItem foodItem)
        //{
        //    // data validation
        //    if (foodItem == null)
        //        throw new ArgumentNullException(nameof(foodItem), "There was item provided to this method");

        //    using (var context = new CCDbContext(
        //        BuildOptions<CCDbContext>()))
        //    {
        //        var newFoodItem = context.FoodItems.Add(foodItem).Entity;
        //        context.SaveChanges();
        //        Log.Debug("Created food item");
        //        return newFoodItem;
        //    }
        //}

        /**
         * <summary>Get a <see cref="FoodItem" by its ID/></summary>
         * <exception cref="ArgumentException">The id was empty</exception>
         * <exception cref="ArgumentNullException">Couldn't retrieve item</exception>
         * <exception cref="InvalidOperationException">Couldn't retrieve item</exception>
         */
        //public FoodItem GetFoodItem(Guid id)
        //{
        //    // data validation
        //    if (id == Guid.Empty)
        //        throw new ArgumentException("There was no id provided to this method");

        //    using (var context = new CCDbContext(
        //        BuildOptions<CCDbContext>()))
        //    {
        //        var newFoodItem = context.FoodItems.SingleOrDefault(item => item.Id == id);
        //        Log.Debug("Found food item");
        //        return newFoodItem;
        //    }
        //}

        /**
         * <summary>Delete a <see cref="FoodItem"/> by id</summary>
         * <exception cref="ArgumentException">The id was empty</exception>
         * <exception cref="InvalidOperationException">A food item with that id does not exist</exception>
         */
        //public void DeleteFoodItem(Guid id)
        //{
        //    using (var context = new CCDbContext(
        //        BuildOptions<CCDbContext>()))
        //    {
        //        FoodItem food = null;
        //        //food = context.FoodItems.First(x => x.Id == id);
        //        context.FoodItems.Remove(food);
        //    }
        //}
        #endregion

        #region Meals
        /**
         * <summary>Create a <see cref="Meal"/> by id</summary>
         * <exception cref="ArgumentNullException">The item was null</exception>
         * <exception cref="DbUpdateException">An error occurred saving to the database</exception>
         * <exception cref="DbUpdateConcurrencyException">An error occurred saving to the database</exception>
         */
        //public Meal CreateMeal(Meal meal)
        //{
        //    // data validation
        //    if (meal == null)
        //        throw new ArgumentNullException(nameof(meal), "There was no item provided to this method");

        //    using (var context = new CCDbContext(
        //        BuildOptions<CCDbContext>()))
        //    {
        //        var newMeal = context.Meals.Add(meal).Entity;
        //        context.SaveChanges();
        //        Log.Debug("Created meal");
        //        return newMeal;
        //    }
        //}
        #endregion

        
        #endregion
    }
}
