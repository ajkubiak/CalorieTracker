using System;
using System.ComponentModel.DataAnnotations;

namespace Lib.Models.Auth
{
    public class User : BaseModel
    {
        [Required]
        public string Username { get; set; }


        public User()
        {
            
        }

        public User(string username)
        {
            Username = username;
        }
    }
}
