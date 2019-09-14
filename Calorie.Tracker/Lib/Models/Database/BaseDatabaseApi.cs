using System.Linq;
using System.Security.Claims;
using Lib.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Lib.Models.Database
{
    public abstract class BaseDatabaseApi
    {
        protected readonly ISettingsUtils settingsUtils;
        public string UserId { get; set; }

        protected BaseDatabaseApi(IHttpContextAccessor httpContextAccessor, ISettingsUtils settingsUtils)
        {
            this.settingsUtils = settingsUtils;

            // Extract username from JWT
            var nameClaim = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).SingleOrDefault();
            if (nameClaim != null)
            {
                UserId = nameClaim.Value;
            }
        }

        protected DbContextOptions<T> BuildOptions<T>() where T : DbContext
        {
            return new DbContextOptionsBuilder<T>()
                .UseLazyLoadingProxies()
                .UseNpgsql(settingsUtils.GetDbConnectionString())
                .Options;
        }
    }
}
