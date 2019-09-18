using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace Lib.Models.Auth
{
    public class User
    {
        private string role;

        [Key]
        [MaxLength(20, ErrorMessage = "Username cannot be longer than 20 characters")]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        /**
         * <exception cref="ArgumentException">The new value is not a valid <see cref="UserRole"/></exception>
         */
        [JsonIgnore]
        public string Role
        {
            get => role;
            set
            {
                if (UserAuthorization.AllRoles.Contains(value))
                {
                    role = value;
                } else
                {
                    throw new ArgumentException($"{value} is not a valid role.");
                }
            }
        }
    }
}
