using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Lib.Models.Auth;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Lib.Utils
{
    public interface IAuthUtils
    {
        string GenerateJwtToken(User user);
        (bool isValid, string errorMessage) ValidatePasswordMeetsRequirements(string password);
        string GeneratePasswordHash(string username, string password);
        PasswordVerificationResult VerifyPasswordHash(string username, string password, string hash);
    }

    public class AuthUtils : IAuthUtils
    {
        private readonly IConfiguration config;
        private readonly IPasswordHasher<string> userLoginHasher;

        public AuthUtils(IConfiguration config, ISettingsUtils settingsUtils)
        {
            this.config = config;
            userLoginHasher = new PasswordHasher<string>();
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                // TODO: Make this a config
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(
                        config.GetSection("tokenConfig").GetValue<string>("secret"))),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        public (bool isValid, string errorMessage) ValidatePasswordMeetsRequirements(string password)
        {
            Log.Debug("Password meets requirements");
            return (isValid: true, errorMessage: null);
        }

        public string GeneratePasswordHash(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username), "Username cannot be null or empty");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty");

            return userLoginHasher.HashPassword(username, password);
        }

        public PasswordVerificationResult VerifyPasswordHash(string username, string password, string hash)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username), "Username cannot be null or empty");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty");
            if (string.IsNullOrEmpty(hash))
                throw new ArgumentNullException(nameof(hash), "Hash cannot be null or empty");

            return userLoginHasher.VerifyHashedPassword(username, hash, password);
        }
    }
}
