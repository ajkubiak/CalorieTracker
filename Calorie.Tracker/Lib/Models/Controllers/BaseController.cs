using System;
using Lib.Models.Database;
using Lib.Models.Exceptions;
using Lib.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Lib.Models.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ISettingsUtils settingsUtils;
        protected readonly IAuthUtils authUtils;

        protected BaseController(ISettingsUtils settingsUtils, IAuthUtils authUtils)
        {
            this.settingsUtils = settingsUtils;
            this.authUtils = authUtils;
        }

        /**
         * <summary>Retrieves an object by id</summary>
         */
        public IActionResult Get<T>(ICrudOps db, Guid id) where T : BaseModel
        {
            if (id == Guid.Empty)
                throw new ArgumentException("There was no id provided to this method");

            try
            {
                var item = db.Read<T>(id);
                if (authUtils.CanAccessEntity(item, HttpContext))
                    return Ok(item);
                return Unauthorized();
            }
            catch (Exception e)
            {
                Log.Error(e, "Couldn't retrieve the item");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult Post<TObject, TDto>(ICrudOps db, TDto itemDto) where TObject : BaseModel where TDto : BaseDto
        {
            Log.Debug("Creating new item: {item}", itemDto);
            if (itemDto == null)
                throw new ArgumentNullException(nameof(itemDto), "The item cannot be null");

            try
            {
                TObject fullItem = (TObject)Activator.CreateInstance(typeof(TObject), itemDto);
                fullItem.OwnedById = authUtils.GetTokenUserId(HttpContext);
                var food = db.Create(fullItem);
                Log.Debug("Created item: ", food);
                return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{food.Id}"), food);
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception creating food item");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult Update<TObject, TDto>(ICrudOps db, TDto udpatedItem) where TObject : BaseModel where TDto : BaseDto
        {
            Log.Debug("Updating item: {item}", udpatedItem);
            if (udpatedItem == null)
                throw new ArgumentNullException(nameof(udpatedItem), "The item cannot be null");

            try
            {
                TObject item = db.Read<TObject>(udpatedItem.Id);
                db.Update(item);
                Log.Debug("Updated the item");
                return NoContent();
            }
            catch (UnauthorizedException ue)
            {
                Log.Debug(ue, "Couldn't access any item with that ID");
                return Unauthorized();
            }
            catch (InvalidOperationException ioe)
            {
                Log.Debug(ioe, "The item was not found.");
                return NotFound();

            }
            catch (Exception e)
            {
                Log.Error(e, "Exception deleting item");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult Delete<T>(ICrudOps db, Guid id) where T : BaseModel
        {
            Log.Debug("Deleting item: {id}", id);
            try
            {
                T item = db.Read<T>(id);
                db.Delete(item);
                Log.Debug("Deleted the item");
                return NoContent();
            }
            catch (UnauthorizedException ue)
            {
                Log.Debug(ue, "Couldn't access any item with that ID");
                return Unauthorized();
            }
            catch (InvalidOperationException ioe)
            {
                Log.Debug(ioe, "The item was not found.");
                return NotFound();

            }
            catch (Exception e)
            {
                Log.Error(e, "Exception deleting item");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
