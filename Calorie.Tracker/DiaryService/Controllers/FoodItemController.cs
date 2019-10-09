using System;
using Lib.Models.Controllers;
using Lib.Models.Database.Diary;
using Lib.Models.Diary;
using Lib.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DiaryService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FoodItemController : BaseController
	{
        private readonly IDiaryDb db;

        public FoodItemController(ISettingsUtils settingsUtils, IAuthUtils authUtils, IDiaryDb db)
            : base(settingsUtils, authUtils)
        {
            this.db = db;
        }

        #region Bulk Operations
        /**
         * <summary>Retrieves food item objects</summary>
         */
        [HttpGet]
		public IActionResult GetFoodItems()
		{
            Log.Debug("Retrieving all food items");
            try
            {
                var foods = db.GetFoodItems();
                Log.Debug("Retrieved the food items");
                return Ok(foods);
            } catch (Exception e)
            {
                Log.Error(e, "Exception retrieving food items");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Single Object Operations
        /**
         * <summary>Creates <see cref="FoodItem"/> objects</summary>
         */
        [HttpPost]
        public IActionResult Post([FromBody]FoodItemDto foodItemDto)
        {
            return base.Post<FoodItem, FoodItemDto>(db, foodItemDto);
        }

        /**
         * <summary>Retrieves <see cref="FoodItem"/> objects</summary>
         */
        [HttpGet("{id}")]
		public IActionResult Get([FromRoute] Guid id)
		{
            return base.Get<FoodItem, FoodItemDto>(db, id);
        }

        /**
         * <summary>Updates a <see cref="FoodItem"/></summary>
         */
        [HttpPut]
		public IActionResult Put(FoodItemDto foodItemDto)
		{
            return base.Update<FoodItem, FoodItemDto>(db, foodItemDto);
		}

        /**
         * <summary>Deletes <see cref="FoodItem"/> objects</summary>
         */
        [HttpDelete("{id}")]
		public IActionResult Delete(Guid id)
		{
            return base.Delete<FoodItem>(db, id);
        }
        #endregion

        [HttpOptions]
        public void Options() { }
	}
}
