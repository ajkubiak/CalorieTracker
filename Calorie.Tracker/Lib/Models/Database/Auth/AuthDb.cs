using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Lib.Models.Auth;
using Lib.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Lib.Models.Database.Auth
{
    public class AuthDb : BaseDatabaseApi, IAuthDb
    {
        private readonly IAuthUtils authUtils;

        public AuthDb(IHttpContextAccessor httpContextAccessor, ISettingsUtils settingsUtils, IAuthUtils authUtils)
            : base(httpContextAccessor, settingsUtils)
        {
            this.authUtils = authUtils;
        }

        /**
         * <summary>Validate login credentials.</summary>
         * <returns>
         *  True if the user is found and valid. False if an exception
         *  occurrs or user is not found.
         * </returns>
         */
        public bool Authenticate(UserLogin userLogin)
        {
            using (var context = new AuthDbContext(
                BuildOptions<AuthDbContext>()))
            {
                Log.Debug("Authenticating: {user}", userLogin.Username);
                try
                {
                    // Find user
                    var foundUserLogin = context.UserLogins
                        .SingleOrDefault(creds =>
                            creds.Username == userLogin.Username);
                    if (foundUserLogin == null)
                    {
                        Log.Debug("User not found");
                        return false;
                    }

                    // Check creds
                    var authResult = authUtils
                            .VerifyPasswordHash(userLogin.Username, userLogin.Password, foundUserLogin.Password);
                    switch(authResult)
                    {
                        case PasswordVerificationResult.Failed:
                            return false;
                        case PasswordVerificationResult.Success:
                            return true;
                        case PasswordVerificationResult.SuccessRehashNeeded:
                            return true;
                        default:
                            return false;
                    }
                } catch(Exception e)
                {
                    Log.Debug(e, "User not authenticated");
                    return false;
                }
            }
        }

        public void CreateUser(UserLogin userLogin)
        {
            Log.Debug("Creating user: {username}", userLogin.Username);
            using (var context = new AuthDbContext(
                BuildOptions<AuthDbContext>()))
            {
                // Hash password
                userLogin.Password = authUtils
                        .GeneratePasswordHash(userLogin.Username, userLogin.Password);

                // Set up user
                userLogin.User.Username = userLogin.Username;

                // Create credentails for authentication
                var createdUserLogin = context.UserLogins.Add(userLogin);
                Log.Debug("Created user login");

                var rowsAffected = context.SaveChanges();
                Log.Debug("Finished setting up new user");
            }
        }

        public User GetUser(string username)
        {
            Log.Debug("Retrieving user: {username}", username);
            using (var context = new AuthDbContext(
                BuildOptions<AuthDbContext>()))
            {
                if (username == null)
                    throw new ArgumentNullException(nameof(username), "User id must be set. You may need to call SetRequestUserId");

                try
                {
                    Log.Debug("!!!!!!!!1 User id is : {id}", username);
                    var foundUser = context.Users
                        .SingleOrDefault(userObj => userObj.Username == username);
                    Log.Debug("Found user: {username}", foundUser.Username);
                    return foundUser;
                }
                catch (Exception e)
                {
                    Log.Debug(e, "User not found");
                    return null;
                }
            }
        }

        public List<User> GetUsers(List<string> userNames)
        {
            Log.Debug("Retrieving {length} users", userNames.Count);
            using (var context = new AuthDbContext(
                BuildOptions<AuthDbContext>()))
            {
                try
                {
                    var foundUsers = context.Users
                        .Where(user => userNames.Contains(user.Username))
                        .ToList();
                    Log.Debug("Found {length} users", foundUsers.Count);
                    return foundUsers;
                }
                catch (Exception e)
                {
                    Log.Debug(e, "Users not found");
                    return null;
                }
            }
        }
    }
}
