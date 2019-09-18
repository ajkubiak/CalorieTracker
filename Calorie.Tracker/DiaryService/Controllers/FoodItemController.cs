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

        public FoodItemController(ISettingsUtils settingsUtils, IDiaryDb db)
            : base(settingsUtils)
        {
            this.db = db;
        }

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

		/**
         * <summary>Retrieves meal objects</summary>
         */
		[HttpGet("{id}")]
		public string Get([FromRoute] Guid id)
		{
			return "value";
		}

		/**
         * <summary>Retrieves meal objects</summary>
         */
		[HttpPost]
		public IActionResult Post([FromBody]FoodItem foodItem)
		{
            Log.Debug("Creating new food item: {item}", foodItem);
            try
            {
                var food = db.CreateFoodItem(foodItem);
                Log.Debug("Created food: ", food);
                return Created(new Uri($"{Request.Scheme}://{Request.Path}/{food.Id}"), food);
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception creating food item");
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
		public IActionResult Delete(Guid id)
		{
            Log.Debug("Deleting food item: {id}", id);
            try
            {
                db.DeleteFoodItem(id);
                Log.Debug("Deleted the food item");
                return NoContent();
            }
            catch (InvalidOperationException ioe)
            {
                Log.Debug(ioe, "The food item was not found.");
                return NotFound();
                    
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception creating food item");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpOptions]
        public void Options() { }
	}
}
