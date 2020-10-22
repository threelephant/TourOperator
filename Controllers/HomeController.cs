using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using TourOperator.Models;

namespace TourOperator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        [Route("user")]
        public ViewResult Users() =>
            View(new Dictionary<string, object>
            { ["Placeholder"] = "Placeholder" });

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("check")]
        public IActionResult Check()
        {
            return Content(User.Identity.Name);
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
