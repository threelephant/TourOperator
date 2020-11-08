using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using TourOperator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TourOperator.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private TourOperatorContext db;
        private readonly ILogger<AccountController> logger;
        public AdminController(TourOperatorContext context, ILogger<AccountController> logger)
        {
            db = context;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Hotel()
        {
            var towns = from town in db.Towns
                        select new SelectListItem { Text = town.Name, Value = town.TownId.ToString() };
            
            ViewBag.Towns = towns;

            return View();
        }
    }
}