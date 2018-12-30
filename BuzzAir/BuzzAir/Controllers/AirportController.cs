using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuzzAir.Data;
using BuzzAir.Models;
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

        public IActionResult CreateAirport()
        {
            return View();
        }

        [HttpPost]
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