using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib.Models.Auth
{
    public class UserLogin : BaseModel
    {
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [ForeignKey("Username")]
        virtual public User User { get; set; }
    }
}
