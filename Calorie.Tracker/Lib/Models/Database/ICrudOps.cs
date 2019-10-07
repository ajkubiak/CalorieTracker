using System;
using Lib.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Lib.Models.Database
{
    public interface ICrudOps
    {
        /**
         * <summary>Create an object</summary>
         * <exception cref="ArgumentNullException">The item was null</exception>
         * <exception cref="DbUpdateException">An error occurred saving to the database</exception>
         * <exception cref="DbUpdateConcurrencyException">An error occurred saving to the database</exception>
         */
        T Create<T>(T newObject) where T : BaseModel;

         /**
          * <summary>Get an object by ID</summary>
          * <exception cref="ArgumentException">The id was empty</exception>
          * <exception cref="ArgumentNullException">Couldn't retrieve item</exception>
          * <exception cref="InvalidOperationException">Couldn't retrieve item</exception>
          * <exception cref="UnauthorizedException">Couldn't access the item</exception>
          */
         T Read<T>(Guid id) where T : BaseModel;

         /**
          * <summary>Update an object. This will update the existing object with the same ID</summary>
          * <exception cref="ArgumentNullException">The item was null</exception>
          * <exception cref="InvalidOperationException">An item with that id does not exist</exception>
          * <exception cref="ApplicationException">An item was found but the user ids didn't match or the item didn't exist</exception>
          * <exception cref="UnauthorizedException">Couldn't access the item</exception>
          */
         T Update<T>(T newObject) where T : BaseModel;

        /**
         * <summary>Delete an object</summary>
         * <exception cref="ArgumentNullException">The item was null</exception>
         * <exception cref="InvalidOperationException">An item with that id does not exist</exception>
         * <exception cref="ApplicationException">An item was found but the user ids didn't match or the item didn't exist</exception>
         * <exception cref="UnauthorizedException">Couldn't access the item</exception>
         */
        void Delete<T>(T item) where T : BaseModel;

        void AddRelationship<TFirst, TSecond, TRelationship>(Guid firstId, Guid secondId) where TFirst : BaseModel where TSecond : BaseModel;
    }
}
