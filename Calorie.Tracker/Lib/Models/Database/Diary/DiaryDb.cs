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
    }
}
