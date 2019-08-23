using Lib.Models;
using Lib.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DiaryService.Controllers
{
    /**
     * <summary>
     *  Controls meals.
     * </summary>
     */
    [Route("api/meals")]
	public class MealsController : Controller
	{
        private readonly ISettingsUtils _settingsUtils;

        public MealsController(ISettingsUtils settingsUtils)
        {
            _settingsUtils = settingsUtils;
        }

        /**
         * <summary>Retrieves meal objects</summary>
         */
        [HttpGet]
		public IActionResult Get()
		{
            using (MealContext db = new MealContext())
            {

            }
			return Ok(_settingsUtils.GetDbConnectionString());
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
