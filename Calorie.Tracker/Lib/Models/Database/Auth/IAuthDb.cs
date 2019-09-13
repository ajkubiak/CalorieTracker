using System;
using System.Collections.Generic;
using Lib.Models.Auth;

namespace Lib.Models.Database.Auth
{
    public interface IAuthDb
    {
        string UserId { get; set; }
        bool Authenticate(UserLogin userLogin);
        void CreateUser(UserLogin userLogin);
        User GetUser(string username);
        List<User> GetUsers(List<string> userNames);
    }
}
