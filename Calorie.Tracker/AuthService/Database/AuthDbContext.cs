﻿using System;
using Lib.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthService.Database
{
    public class AuthDbContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<User> Users { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
            // empty, needed for creating migrations
        }
    }
}
