using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuzzAir.Data;
using BuzzAir.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Controllers
{
    public class AircraftController : Controller
    {
        private AppDbContext db;

        public AircraftController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult CreateAircraft()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AircraftCreateViewModel model)
        {
            var aircraft = new Aircraft
            {
                Name = model.Name,
                NumberOfSeats = int.Parse(model.Seats)
            };

            this.db.Aircrafts.Add(aircraft);
            this.db.SaveChanges();

            return this.Redirect("/");
        }
    }
}