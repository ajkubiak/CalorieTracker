using System;
using System.Collections.Generic;
using Lib.Models.Auth;
using Lib.Models.Controllers;
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
        private readonly IAuthDb db;

        public UserController(ISettingsUtils settingsUtils, IAuthDb db, IAuthUtils authUtils)
            : base(settingsUtils)
        {
            this.authUtils = authUtils;
            this.db = db;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserLogin userLogin)
        {
            Log.Debug("Creating user: {username}", userLogin.Username);
            try
            {
                var (isValid, errorMessage) = authUtils
                        .ValidatePasswordMeetsRequirements(userLogin.Password);
                if (!isValid)
                    return BadRequest($"Password does not meet requirements: {errorMessage}");

                // IMPORTANT: set user role 
                userLogin.User.Role = UserAuthorization.USER;

                db.CreateUser(userLogin);
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception creating user");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
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
                Log.Error(e, "Exception retrieving food items");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Authorize(Policy = UserAuthorization.POLICY_ADMIN_ONLY)]
        public IActionResult GetUsers([FromQuery] List<string> usernames)
        {
            Log.Debug("Getting users");
            try
            {
                var userList = db.GetUsers(usernames);
                return Ok(userList);
            } catch (Exception e)
            {
                Log.Error(e, "Exception while getting users");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
