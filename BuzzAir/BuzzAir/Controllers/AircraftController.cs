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
    public class AircraftController : Controller
    {
        private AppDbContext db;

        public AircraftController(AppDbContext db)
        {
            this.db = db;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateAircraft()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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