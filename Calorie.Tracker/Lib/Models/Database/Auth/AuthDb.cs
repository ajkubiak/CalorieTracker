using System;
using System.Linq;
using Lib.Models.Auth;
using Lib.Utils;
using Serilog;

namespace Lib.Models.Database.Auth
{
    public class AuthDb : BaseDatabaseApi, IAuthDb
    {
        public AuthDb(ISettingsUtils settingsUtils)
            : base(settingsUtils)
        {
        }

        public bool Authenticate(UserLogin userLogin)
        {
            using (var context = new AuthDbContext(
                BuildOptions<AuthDbContext>()))
            {
                Log.Debug("Authenticating: {user}", userLogin.Username);
                try
                {
                    var login = context.UserLogins
                        .SingleOrDefault(creds =>
                            creds.Id == userLogin.Id
                            && creds.Password == userLogin.Password);
                    return login != null;
                } catch(Exception e)
                {
                    Log.Debug("User not authenticated", e);
                    return false;
                }
            }
        }
    }
}
