using System;
using Lib.Models.Auth;

namespace Lib.Models.Database.Auth
{
    public interface IAuthDb
    {
        bool Authenticate(UserLogin userLogin);
    }
}
