using System;
using Lib.Models.Controllers;
using Lib.Models.Database.Diary;
using Lib.Models.Diary;
using Lib.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        #endregion

        [HttpOptions]
        public void Options() { }
	}
}
