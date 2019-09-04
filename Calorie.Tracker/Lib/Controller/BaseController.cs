using System;
using DiaryService.Database;
using Lib.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiaryService.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ISettingsUtils settingsUtils;
        protected readonly IDatabaseApi db;

        protected BaseController(ISettingsUtils settingsUtils, IDatabaseApi db)
        {
            this.settingsUtils = settingsUtils;
            this.db = db;
        }
    }
}
