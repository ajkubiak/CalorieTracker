using System;
using System.Collections.Generic;
using Lib.Models.Auth;

namespace Lib.Models.Database.Auth
{
    public interface IAuthDb
    {
        bool Authenticate(UserLogin userLogin);
        public void CreateUser(UserLogin userLogin);
        User GetUser(string username);
        List<User> GetUser(List<string> userNames);
    }
}
