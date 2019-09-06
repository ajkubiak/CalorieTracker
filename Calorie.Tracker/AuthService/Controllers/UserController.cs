using System;
using Lib.Models.Auth;
using Lib.Models.Database.Auth;
using Lib.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IAuthUtils authUtils;

        public UserController(IAuthUtils authUtils, ISettingsUtils settingsUtils, IAuthDb db)
            : base(settingsUtils, db)
        {
            this.authUtils = authUtils;
        }



        [AllowAnonymous]
        [Consumes("application/json")]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLogin userLogin)
        {
            Log.Debug("Received request to authenticate: {user}", userLogin.Username);
            try
            {
                bool isAuthenticated = db.Authenticate(userLogin);
                Log.Debug("isAuthenticated: {username}: {auth}", userLogin.Username, isAuthenticated);
                if (isAuthenticated)
                {
                    var user = db.GetUser(userLogin.Username);
                    if (user == null)
                    {
                        return new NotFoundResult();
                    }
                    return Ok(authUtils.GenerateJwtToken(user));
                }
                return new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            catch (Exception e)
            {
                Log.Error("Exception retrieving food items", e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
