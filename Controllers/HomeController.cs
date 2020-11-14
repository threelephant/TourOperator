using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using TourOperator.Models;

namespace TourOperator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly TourOperatorContext db;

        public HomeController(ILogger<HomeController> logger, TourOperatorContext db)
        {
            this.logger = logger;
            this.db = db;
        }

        [Route("user")]
        public ViewResult Users() =>
            View(new Dictionary<string, object>
            { ["Placeholder"] = "Placeholder" });

        public IActionResult Index()
        {
            return View();
        }
        
        [Route("hotels")]
        public IActionResult Hotels(int start = 0, int size = 5)
        {
            ViewBag.Start = start;
            ViewBag.Size = size;

            int pagesCount = (int) Math.Ceiling(db.Hotels.Count() / (double) size);
            ViewBag.PagesCount = pagesCount;

            int currentPage = start / size + 1;
            ViewBag.CurrentPage = currentPage;

            ViewBag.Hotels = db.Hotels.OrderBy(h => h.Name)
                                      .Skip(start)
                                      .Take(size)
                                      .ToList();

            return View();
        }

        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
