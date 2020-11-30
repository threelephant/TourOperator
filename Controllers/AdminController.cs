using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using TourOperator.Models;
using TourOperator.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace TourOperator.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly TourOperatorContext db;
        private readonly ILogger<AccountController> logger;
        private readonly IWebHostEnvironment appEnvironment;
        public AdminController(TourOperatorContext context, 
                               ILogger<AccountController> logger, 
                               IWebHostEnvironment appEnvironment)
        {
            db = context;
            this.logger = logger;
            this.appEnvironment = appEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Hotel()
        {
            var towns = from town in db.Towns
                select new SelectListItem { Text = town.Name, Value = town.TownId.ToString() };
            
            var raitings = from raiting in db.Raitings
                select new SelectListItem { Text = raiting.Name, Value = raiting.RaitingId.ToString() };

            var foods = from food in db.Foods
                select new SelectListItem {Text = food.Name, Value = food.FoodId.ToString()};
            
            ViewBag.Towns = towns.ToList();
            ViewBag.Raitings = raitings.ToList();
            ViewBag.Foods = foods.ToList();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Hotel(Hotel hotel, IFormFile uploadedFile)
        {
            string path = "/img/" + uploadedFile.FileName;
            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }

            hotel.Image = path;

            db.Hotels.Add(hotel);
            db.SaveChanges();
            
            return Hotel();
        }
        
        [HttpGet]
        public IActionResult Country()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Country(Country country)
        {
            db.Countries.Add(country);
            db.SaveChanges();
            
            return Country();
        }
        
        [HttpGet]
        public IActionResult Town()
        {
            var countries = from country in db.Countries
                select new SelectListItem { Text = country.Name, Value = country.CountryId.ToString() };

            ViewBag.Countries = countries.ToList();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Town(Town town)
        {
            db.Towns.Add(town);
            db.SaveChanges();
            
            return Town();
        }
        
        [HttpGet]
        public IActionResult Airport()
        {
            var towns = from country in db.Towns
                select new SelectListItem { Text = country.Name, Value = country.TownId.ToString() };

            ViewBag.Towns = towns.ToList();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Airport(Airport airport)
        {
            db.Airports.Add(airport);
            db.SaveChanges();
            
            return Airport();
        }
        
        [HttpGet]
        public IActionResult Flight()
        {
            var airports = from airport in db.Airports
                select new SelectListItem { Text = airport.Name, Value = airport.AirportId.ToString() };

            ViewBag.Airports = airports.ToList();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Flight(Flight flight)
        {
            db.Flights.Add(flight);
            db.SaveChanges();
            
            return Flight();
        }

        [HttpGet]
        public IActionResult File()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> File(IFormFile uploadedFile)
        {
            string path = "/img/" + uploadedFile.FileName;
            using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult Admins()
        {
            var users = db.Users.Include(u => u.Role)
                                .OrderByDescending(u => u.Role.Name)
                                    .ThenBy(u => u.Login);
            ViewBag.Users = users;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Admins(long userId, bool toAdmin = false)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (toAdmin)
            {
                user.RoleId = 1;
            }
            else
            {
                user.RoleId = 2;
            }
            
            await db.SaveChangesAsync();

            return RedirectToAction("Admins");
        }

        [HttpGet]
        public IActionResult Stats()
        {
            var avgUserRating = GetAvgUserRating();

            ViewBag.Labels = avgUserRating.Item1;
            ViewBag.Data = avgUserRating.Item2;

            var countUserTours = GetCountUserTours();

            ViewBag.LabelsTours = countUserTours.Item1;
            ViewBag.DataTours = countUserTours.Item2;
            
            return View();
        }

        private (string, string) GetAvgUserRating()
        {
            var groupsReviews = from hotel in db.Hotels
                                join review in db.Reviews on hotel equals review.Hotel
                                group review by hotel.Name into g
                                select new { 
                                                Name = g.Key, 
                                                Average = g.Average(g => g.UserRaiting)
                                            };

            var labelsReviews = groupsReviews.Select(t => t.Name).ToList();
            var dataReviews = groupsReviews.Select(t => t.Average).ToList();

            var jsonLabelsReviews = JsonSerializer.Serialize(labelsReviews);
            var jsonDataReviews = JsonSerializer.Serialize(dataReviews);

            return (jsonLabelsReviews, jsonDataReviews);
        }

        private (string, string) GetCountUserTours()
        {
            var groupsTours = from hotel in db.Hotels
                              join tour in db.Tours on hotel equals tour.Hotel
                              group tour by hotel.Name into g
                              select new { 
                                              Name = g.Key, 
                                              Count = g.Count()
                                         };

            var labelsTours = groupsTours.Select(t => t.Name).ToList();
            var dataTours = groupsTours.Select(t => t.Count).ToList();

            var jsonLabelsTours = JsonSerializer.Serialize(labelsTours);
            var jsonDataTours = JsonSerializer.Serialize(dataTours);

            return (jsonLabelsTours, jsonDataTours);
        }
    }
}