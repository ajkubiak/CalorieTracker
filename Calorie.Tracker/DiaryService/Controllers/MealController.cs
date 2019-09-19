using System;
using Lib.Models.Controllers;
using Lib.Models.Database;
using Lib.Models.Database.Diary;
using Lib.Models.Diary;
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
        private readonly ICrudOps db;

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
            //Log.Debug("Creating new meal: {item}", mealDto);
            //if (mealDto == null)
            //    throw new ArgumentNullException(nameof(mealDto), "The meal cannot be null");

            //try
            //{
            //    Meal meal = new Meal(mealDto)
            //    {
            //        OwnedById = authUtils.GetTokenUserId(HttpContext)
            //    };
            //    var createdMeal = db.Create(meal);
            //    Log.Debug("Created meal: {meal}", createdMeal);
            //    return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{createdMeal.Id}"), createdMeal);
            //}
            //catch (Exception e)
            //{
            //    Log.Error(e, "Exception creating meal");
            //    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            //}
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
        #endregion

        [HttpOptions]
        public void Options() { }
	}
}
