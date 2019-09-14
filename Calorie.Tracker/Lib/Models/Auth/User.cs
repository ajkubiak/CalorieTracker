using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Lib.Models.Auth
{
    public class User
    {
        [Key]
        [MaxLength(20)]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public User()
        {
            
        }

        public User(string username)
        {
            Username = username;
        }
    }
}
