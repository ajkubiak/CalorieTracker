using System;
using Lib.Models.Exceptions;
using Lib.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Lib.Models.Database
{
    public abstract class BaseDatabaseApi
    {
        protected readonly ISettingsUtils settingsUtils;
        protected readonly IAuthUtils authUtils;
        protected readonly IHttpContextAccessor httpContextAccessor;

        protected BaseDatabaseApi(IHttpContextAccessor httpContextAccessor, ISettingsUtils settingsUtils, IAuthUtils authUtils)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.settingsUtils = settingsUtils;
            this.authUtils = authUtils;
        }

        protected DbContextOptions<T> BuildOptions<T>() where T : DbContext
        {
            return new DbContextOptionsBuilder<T>()
                .UseLazyLoadingProxies()
                .UseNpgsql(settingsUtils.GetDbConnectionString())
                .Options;
        }

        #region CRUD ops
        public T Create<T>(T newObject) where T : BaseModel
        {
            // data validation
            if (newObject == null)
                throw new ArgumentNullException(nameof(newObject), $"There was no object provided to this method. Type was: {typeof(T)}");

            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                var createdObject = context.Add(newObject).Entity;
                context.SaveChanges();
                Log.Debug("Created new object");
                return createdObject;
            }
        }

        public T Read<T>(Guid id) where T : BaseModel
        {
            // data validation
            if (id == Guid.Empty)
                throw new ArgumentException("There was no id provided to this method");

            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                var retrievedItem = context.Find<T>(id);
                Log.Debug("Found item");

                if (authUtils.CanAccessEntity(retrievedItem, httpContextAccessor.HttpContext))
                {
                    return retrievedItem;
                }
                throw new UnauthorizedException("The user does not have access to this item or the item was not found");
            }
        }

        public T Update<T>(T newObject) where T : BaseModel
        {
            // data validation
            if (newObject == null)
                throw new ArgumentNullException(nameof(newObject), "The item was null");

            using(var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                // Check is owned by this user
                if (authUtils.CanAccessEntity(newObject, httpContextAccessor.HttpContext))
                {
                    context.Update(newObject);
                    int rowsAffected = context.SaveChanges();
                    Log.Debug("{rows} rows were affected by this update", rowsAffected);
                    return newObject;
                }
                throw new UnauthorizedException("The user does not have access to this item or the item was not found");
            }
        }

        public void Delete<T>(T item) where T : BaseModel
        {
            // data validation
            if (item == null)
                throw new ArgumentNullException(nameof(item), "The item was null");

            using (var context = new CCDbContext(
                BuildOptions<CCDbContext>()))
            {
                // Check is owned by this user
                if (authUtils.CanAccessEntity(item, httpContextAccessor.HttpContext))
                {
                    context.Remove(item);
                    int rowsAffected = context.SaveChanges();
                    Log.Debug("{rows} rows were affected by this delete", rowsAffected);
                } else
                {
                    throw new UnauthorizedException("The user does not have access to this item or the item was not found");
                }
            }
        }
        #endregion
    }
}
