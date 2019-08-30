using System;
using Lib.Models.Auth;

namespace AuthService.Database
{
    public interface IAuthDb
    {
        bool Authenticate(UserLogin userLogin);
    }
}
