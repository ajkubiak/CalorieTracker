using System;
using System.Collections.Generic;
using System.Linq;
using Lib.Models.Auth;
using Lib.Utils;
using Serilog;

namespace Lib.Models.Database.Auth
{
    public class AuthDb : BaseDatabaseApi, IAuthDb
    {
        public string UserId { get; set; }

        public AuthDb(ISettingsUtils settingsUtils)
            : base(settingsUtils)
        {
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
                    // Hash password

                    // Check creds
                    return context.UserLogins
                        .SingleOrDefault(creds =>
                            creds.Username == userLogin.Username
                            && creds.Password == userLogin.Password) != null;
                } catch(Exception e)
                {
                    Log.Debug("User not authenticated", e);
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

                // Create credentails for authentication
                context.UserLogins.Add(userLogin);
                Log.Debug("Created user login");

                // Create User
                context.Users.Add(new User(userLogin.Username));
                Log.Debug("Created new user");
                // Commit changes
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
                if (UserId == null)
                    throw new ArgumentNullException("User id must be set. You may need to call SetRequestUserId");

                try
                {
                    Log.Debug("!!!!!!!!1 User id is : {id}", UserId);
                    var foundUser = context.Users
                        .SingleOrDefault(userObj => userObj.Username == username);
                    Log.Debug("Found user: {username}", foundUser.Username);
                    return foundUser;
                }
                catch (Exception e)
                {
                    Log.Debug("User not found", e);
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
                    Log.Debug("Users not found", e);
                    return null;
                }
            }
        }
    }
}
