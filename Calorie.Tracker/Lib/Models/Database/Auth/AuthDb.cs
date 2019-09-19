using System;
using System.Collections.Generic;
using System.Linq;
using Lib.Models.Auth;
using Lib.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Lib.Models.Database.Auth
{
    public class AuthDb : BaseDatabaseApi, IAuthDb
    {
        public AuthDb(IHttpContextAccessor httpContextAccessor, ISettingsUtils settingsUtils, IAuthUtils authUtils)
            : base(httpContextAccessor, settingsUtils, authUtils)
        {
            // empty
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
            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
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
                            AddUserToDb(context, userLogin, isUpdate: true);
                            return true;
                        default:
                            return false;
                    }
                }
                catch (System.Exception e)
                {
                    Log.Debug(e, "User not authenticated");
                    return false;
                }
            }
        }

        public void CreateUser(UserLogin userLogin)
        {
            Log.Debug("Creating user: {username}", userLogin.Username);
            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                try
                {
                    AddUserToDb(context, userLogin, isUpdate:false);
                }
                catch (DbUpdateException e)
                {
                    Log.Error(e, "Error saving changes do dbcontext.");
                    throw e;
                }
            }
        }

        public User GetUser(string username)
        {
            Log.Debug("Retrieving user: {username}", username);
            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                if (username == null)
                    throw new ArgumentNullException(nameof(username), "User id must be set. You may need to call SetRequestUserId");

                try
                {
                    var foundUser = context.Users
                        .SingleOrDefault(userObj => userObj.Username == username);
                    Log.Debug("Found user: {username}", foundUser.Username);
                    return foundUser;
                }
                catch (ArgumentNullException e)
                {
                    Log.Debug(e, "The user couldn't be retrieved since an argument was null");
                    return null;
                }
                catch (InvalidOperationException e)
                {
                    Log.Debug(e, "An illegal operation was performed while retrieving user");
                    return null;
                }
            }
        }

        public IList<User> GetUsers(IList<string> userNames)
        {
            Log.Debug("Retrieving {length} users", userNames.Count);
            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                try
                {
                    if (userNames.Count > 0)
                    {
                        return context.Users
                            .Where(user => userNames.Contains(user.Username))
                            .ToList();
                    }
                    return context.Users.ToList();
                }
                catch (ArgumentNullException e)
                {
                    Log.Debug(e, "Users could not be retrieved since an argument was null");
                    return null;
                }
            }
        }

        #region Private functions
        /**
         * <summary>Add or update a <see cref="UserLogin"/> in the db</summary>
         * <param name="context">The database context for <see cref="UserLogin"/></param>
         * <param name="userLogin">The user's login credentials</param>
         * <param name="isUpdate">
         *  Whether the user already exists in the db. True to update
         *  and existing user
         * </param>
         */
        private void AddUserToDb(CCDbContext context, UserLogin userLogin, bool isUpdate)
        {
            // Hash password
            userLogin.Password = authUtils
                    .GeneratePasswordHash(userLogin.Username, userLogin.Password);

            // Set up user
            userLogin.User.Username = userLogin.Username;

            // Create creds for authentication, update if user exists
            _ = isUpdate
                ? context.UserLogins.Update(userLogin)
                : context.UserLogins.Add(userLogin);
            Log.Debug("Added user login");

            context.SaveChanges();
            Log.Debug("Finished setting up new user");
        }
        #endregion
    }
}
