using System;
namespace Lib.Models.Auth
{
    public class UserLogin : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserLogin()
        {

        }
    }
}
