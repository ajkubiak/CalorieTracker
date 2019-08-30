using System;
using System.ComponentModel.DataAnnotations;

namespace Lib.Models.Auth
{
    public class User : BaseModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
        public User()
        {
            
        }
    }
}
