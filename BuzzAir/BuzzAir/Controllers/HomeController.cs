using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuzzAir.Data;
using BuzzAir.Models;

namespace BuzzAir.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(AppDbContext _context)
        {
            context = _context;
        }

        private readonly AppDbContext context;

        public IActionResult Search(string origin, string destination, int passengersNumber)
        {
            var model = new SearchModel();
            model.Origin = origin;
            model.Destination = destination;
            model.PassengersNumber = passengersNumber;

            if (!context.Flights.Any(x => x.Origin.Name == model.Origin && x.Destination.Name == model.Destination))
            {
                model.Error = true;
                model.ErrorMessage = "No such flights";
            }

            return View(model);
        }

        public IActionResult Index(IndexModel model)
        {
            model.Airports = this.context.Airports.ToList();
            model.Flights = this.context.Flights.ToList();
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

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
