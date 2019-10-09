using System;
using System.Collections.Generic;
using System.Linq;
using Lib.Models.Diary;
using Lib.Utils;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Lib.Models.Database.Diary
{
    public class DiaryDb : BaseDatabaseApi, IDiaryDb
    {
        public DiaryDb(IHttpContextAccessor httpContextAccessor, ISettingsUtils settingsUtils, IAuthUtils authUtils)
            : base(httpContextAccessor, settingsUtils, authUtils)
        {
            // empty
        }

        #region Relationships
        public void AddManytoMany<TRelationship>(Guid firstId, Guid secondId) where TRelationship : IManyToMany, new()
        {
            TRelationship rel = new TRelationship();
            rel.SetIdFirst(firstId);
            rel.SetIdSecond(secondId);
            Log.Debug("Adding a new '{relationship}' relationship: {object}", nameof(TRelationship), rel);
            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                context.Add(rel);
                context.SaveChanges();
            }
        }
        #endregion

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
