using Lib.Models.Database.Auth;
using Lib.Utils;
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
    }
}
