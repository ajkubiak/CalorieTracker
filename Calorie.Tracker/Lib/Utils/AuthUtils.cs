using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lib.Models.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Lib.Utils
{
    public interface IAuthUtils
    {
        //static Action<AuthenticationOptions> AddAuthentiction();
        //static Action<JwtBearerOptions> AddAuthorization();

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
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
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
            // TODO: Actually write an implementation

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

        public static Action<AuthenticationOptions> AddAuthentiction()
        {
            return options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            };
        }

        public static Action<JwtBearerOptions> AddJwtBearer(IConfiguration config)
        {
            return options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                            config.GetSection("tokenConfig").GetValue<string>("secret"))),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            };
        }

        public static Action<AuthorizationOptions> AddAuthorization()
        {
            return options =>
            {
                options.AddPolicy(UserAuthorization.POLICY_ADMIN_ONLY, policy => policy.RequireClaim(ClaimTypes.Role, UserAuthorization.ADMIN));
            };
        }
    }
}
