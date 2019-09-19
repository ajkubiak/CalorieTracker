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
            //Log.Debug("Creating new food item: {item}", foodItemDto);
            //if (foodItemDto == null)
            //    throw new ArgumentNullException(nameof(foodItemDto), "The food item cannot be null");

            //try
            //{
            //    FoodItem foodItem = new FoodItem(foodItemDto)
            //    {
            //        OwnedById = authUtils.GetTokenUserId(HttpContext)
            //    };
            //    var food = db.Create(foodItem);
            //    Log.Debug("Created food: ", food);
            //    return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{food.Id}"), food);
            //}
            //catch (Exception e)
            //{
            //    Log.Error(e, "Exception creating food item");
            //    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            //}
            return base.Post<FoodItem, FoodItemDto>(db, foodItemDto);
        }

        /**
         * <summary>Retrieves <see cref="FoodItem"/> objects</summary>
         */
        [HttpGet("{id}")]
		public IActionResult Get([FromRoute] Guid id)
		{
            //if (id == Guid.Empty)
            //    throw new ArgumentException("There was no id provided to this method");
            //try
            //{
            //    var item = db.Read<FoodItem>(id);
            //    if (authUtils.CanAccessEntity(item, HttpContext))
            //        return Ok(item);
            //    return Unauthorized();
            //}
            //catch (Exception e)
            //{
            //    Log.Error(e, "Couldn't retrieve the item");
            //    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            //}
            return base.Get<FoodItem>(db, id);
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
            //Log.Debug("Deleting food item: {id}", id);
            //try
            //{
            //    FoodItem item = db.Read<FoodItem>(id);
            //    db.Delete(item);
            //    Log.Debug("Deleted the food item");
            //    return NoContent();
            //}
            //catch (UnauthorizedException ue)
            //{
            //    Log.Debug(ue, "Couldn't access any item with that ID");
            //    return Unauthorized();
            //}
            //catch (InvalidOperationException ioe)
            //{
            //    Log.Debug(ioe, "The food item was not found.");
            //    return NotFound();

            //}
            //catch (Exception e)
            //{
            //    Log.Error(e, "Exception creating food item");
            //    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            //}
            return base.Delete<FoodItem>(db, id);
        }
        #endregion

        [HttpOptions]
        public void Options() { }
	}
}
