using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuzzAir.Data;
using BuzzAir.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Controllers
{
    public class AirportController : Controller
    {
        private AppDbContext db;

        public AirportController(AppDbContext db)
        {
            this.db = db;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateAirport()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(AirportCreateViewModel model)
        {
            Enum.TryParse(model.Country, out Country country);
            var airport = new Airport
            {
                Name = model.Name,
                City = model.City,
                Country = country,
                Terminal = model.Terminal
            };

            this.db.Airports.Add(airport);
            this.db.SaveChanges();
            return this.Redirect("/");
        }
    }
}