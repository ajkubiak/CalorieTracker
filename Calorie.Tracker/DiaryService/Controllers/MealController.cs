using System;
using Lib.Models.Controllers;
using Lib.Models.Database.Diary;
using Lib.Models.Diary;
using Lib.Models.Exceptions;
using Lib.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DiaryService.Controllers
{
    /**
     * <summary>
     *  Controls meals.
     * </summary>
     */
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MealController : BaseController
	{
        private readonly IDiaryDb db;

        public MealController(ISettingsUtils settingsUtils, IAuthUtils authUtils, IDiaryDb db)
            : base(settingsUtils, authUtils)
        {
            this.db = db;
        }

        #region Bulk Operations
        
        #endregion

        #region Single Object Operations
        /**
         * <summary>Creates a <see cref="Meal"/></summary>
         */
        [HttpPost]
        public IActionResult Post([FromBody] MealDto mealDto)
        {
            return base.Post<Meal, MealDto>(db, mealDto);
        }

        /**
         * <summary>Retrieves <see cref="Meal"/> objects</summary>
         */
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            return base.Get<Meal>(db, id);
        }

        /**
         * <summary>Updates <see cref="Meal"/></summary>
         */
        [HttpPut]
        public IActionResult Put(MealDto mealDto)
        {
            return base.Update<Meal, MealDto>(db, mealDto);
        }

        /**
         * <summary>Deletes <see cref="Meal"/></summary>
         */
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            return base.Delete<Meal>(db, id);
        }

        /**
         * <summary>Adds a <see cref="FoodItem"/> to a <see cref="Meal"/></summary>
         */
        public IActionResult AddFoodToMeal(Guid mealId, Guid foodId)
        {
            Log.Debug("Adding... meal:{mealid}, food:{foodid}", mealId, foodId);
            if (mealId == Guid.Empty || foodId == Guid.Empty)
                return new StatusCodeResult(StatusCodes.Status422UnprocessableEntity);

            try
            {
                //Meal meal = db.Read<Meal>(mealId);
                //FoodItem item = db.Read<FoodItem>(foodId);
                //var relationship = new FoodItemMeal()
                //{
                //    Meal = meal,
                //    FoodItem = item
                //};
                //db.Create<FoodItemMeal>(relationship);
                db.AddRelationship<Meal, FoodItem, FoodItemMeal>(mealId, foodId);
            }
            catch (UnauthorizedException ue)
            {
                Log.Debug(ue, "Couldn't access any item with that ID");
                return Unauthorized();
            }
            catch (InvalidOperationException ioe)
            {
                Log.Debug(ioe, "The item was not found.");
                return NotFound();

            }
            catch (Exception e)
            {
                Log.Error(e, "Exception deleting item");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        [HttpOptions]
        public void Options() { }
	}
}
