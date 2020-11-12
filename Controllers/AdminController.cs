using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using TourOperator.Models;
using TourOperator.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TourOperator.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly TourOperatorContext db;
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
        public IActionResult Hotel(Hotel hotel)
        {
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
    }
}