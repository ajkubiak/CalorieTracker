using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [AllowAnonymous]
        [Consumes("application/json")]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLogin userLogin)
        {
            return Ok(userLogin);
        }
    }
}
