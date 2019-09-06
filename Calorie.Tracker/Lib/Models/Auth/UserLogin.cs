using System;
using System.ComponentModel.DataAnnotations;

namespace Lib.Models.Auth
{
    public class UserLogin : BaseModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public UserLogin()
        {

        }
    }
}
