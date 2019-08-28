using System.Linq;
using DiaryService.Database.EfContext;
using Lib.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DiaryService.Controllers
{
    /**
     * <summary>
     *  Controls food items.
     * </summary>
     */
    [Route("api/fooditems")]
	public class FoodItemController : Controller
	{
        private readonly ISettingsUtils _settingsUtils;
        public FoodItemController(ISettingsUtils settingsUtils)
        {
            _settingsUtils = settingsUtils;
        }

        /**
         * <summary>Retrieves food item objects</summary>
         */
        [HttpGet]
		public IActionResult Get()
		{
            using (var context = new FoodItemContext(
                    _settingsUtils.BuildOptions<FoodItemContext>()))
            {
                var foods = context.FoodItems.ToList();
                return Ok(foods);
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
		public void Post([FromBody]string value)
		{
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
		public void Delete(int id)
		{
		}

        [HttpOptions]
        public void Options() { }
	}
}
