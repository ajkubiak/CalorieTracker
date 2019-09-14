using System.Linq;
using System.Security.Claims;
using Lib.Models.Database;
using Lib.Models.Database.Auth;
using Lib.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lib.Models.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ISettingsUtils settingsUtils;

        protected BaseController(ISettingsUtils settingsUtils)
        {
            this.settingsUtils = settingsUtils;
        }
    }
}
