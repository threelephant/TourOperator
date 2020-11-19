using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using TourOperator.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [Route("hotel/{id}")]
        public IActionResult Hotel(int id)
        {
            var hotel = db.Hotels.Include(h => h.Raiting)
                                 .Include(h => h.Food)
                                 .Include(h => h.Reviews)
                                    .ThenInclude(r => r.User)
                                 .FirstOrDefault(h => h.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }

            var r = db.Reviews.FirstOrDefault(r => r.HotelId == id);
            if (r != null)
            {
                var users = from review in hotel.Reviews
                        select review.User.Login; 
            
                ViewBag.IsCommented = users.Contains(User.Identity.Name);
            }
            else
            {
                ViewBag.IsCommented = false;
            }
            ViewBag.Hotel = hotel;

            return View();
        }

        [HttpPost]
        public IActionResult Review(Review review)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == User.Identity.Name);
            review.UserId = user.UserId;
            
            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("Hotels", "Home");
        }

        [HttpGet]
        [Route("tour/{hotelId}")]
        public IActionResult Tour(int hotelId)
        {
            ViewBag.HotelId = hotelId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTour(Tour tour)
        {
            var hotelPricePerNight = (await db.Hotels.FirstOrDefaultAsync(h => h.HotelId == tour.HotelId)).PricePerNight;
            
            tour.Price = hotelPricePerNight * (tour.AdultNumber + tour.ChildNumber / 2);
            tour.User = await db.Users.FirstOrDefaultAsync(u => u.Login == User.Identity.Name);
            tour.TimeBooking = DateTime.Now;

            await db.Tours.AddAsync(tour);
            await db.SaveChangesAsync();

            return RedirectToAction("Hotels", "Home");
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
