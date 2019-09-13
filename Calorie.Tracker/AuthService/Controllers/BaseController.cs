using System.Linq;
using System.Security.Claims;
using Lib.Models.Database.Auth;
using Lib.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ISettingsUtils settingsUtils;
        protected readonly IAuthDb db;

        protected BaseController(ISettingsUtils settingsUtils, IAuthDb db)
        {
            this.settingsUtils = settingsUtils;
            this.db = db;
        }

        protected void SetRequestUserId(HttpContext httpContext)
        {
            var nameClaim = httpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).SingleOrDefault();
            if (nameClaim != null)
            {
                db.UserId = nameClaim.Value;
            } else
            {
                throw new System.Exception("The user id could not be retrieved from the request");
            }
        }
    }
}
