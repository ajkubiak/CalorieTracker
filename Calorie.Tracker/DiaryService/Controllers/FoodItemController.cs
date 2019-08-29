using System;
using DiaryService.Database;
using Lib.Models;
using Lib.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DiaryService.Controllers
{
    /**
     * <summary>
     *  Controls food items.
     * </summary>
     */
    [Route("api/fooditems")]
	public class FoodItemController : BaseController
	{
        public FoodItemController(ISettingsUtils settingsUtils, IDatabaseApi db)
            : base(settingsUtils, db)
        {

        }

        /**
         * <summary>Retrieves food item objects</summary>
         */
        [HttpGet]
		public IActionResult Get()
		{
            Log.Debug("Retrieving all food items");
            try
            {
                var foods = db.GetFoodItems();
                Log.Debug("Retrieved the food items");
                return Ok(foods);
            } catch (Exception e)
            {
                Log.Error("Exception retrieving food items", e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

		/**
         * <summary>Retrieves meal objects</summary>
         */
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		/**
         * <summary>Retrieves meal objects</summary>
         */
		[HttpPost]
		public IActionResult Post([FromBody]FoodItem foodItem)
		{
            Log.Debug("Creating new food item", foodItem);
            try
            {
                var food = db.CreateFoodItem(foodItem);
                Log.Debug("Created food: ", food);
                return Created(new Uri($"{Request.Path}/{food.Id}"), food);
            }
            catch (Exception e)
            {
                Log.Error("Exception creating food item", e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

		/**
         * <summary>Retrieves meal objects</summary>
         */
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		/**
         * <summary>Retrieves meal objects</summary>
         */
		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
            Log.Debug("Deleting food item: ", id);
            try
            {
                db.DeleteFoodItem(id);
                Log.Debug("Deleted the food item");
                return NoContent();
            }
            catch (InvalidOperationException ioe)
            {
                Log.Debug("The food item was not found.", ioe);
                return NotFound();
                    
            }
            catch (Exception e)
            {
                Log.Error("Exception creating food item", e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpOptions]
        public void Options() { }
	}
}
