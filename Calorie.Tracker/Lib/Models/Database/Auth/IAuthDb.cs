﻿using System;
using System.Collections.Generic;
using Lib.Models.Auth;

namespace Lib.Models.Database.Auth
{
    public interface IAuthDb
    {
        bool Authenticate(UserLogin userLogin);
        void CreateUser(UserLogin userLogin);
        User GetUser(string username);
        IList<User> GetUsers(IList<string> userNames);
    }
}
