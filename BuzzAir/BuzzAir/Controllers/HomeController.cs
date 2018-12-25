using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuzzAir.Data;
using BuzzAir.Models;
using Microsoft.AspNetCore.Identity;

namespace BuzzAir.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(AppDbContext _context)
        {
            context = _context;
        }

        private readonly AppDbContext context;

        public IActionResult Index(IndexModel model)
        {
            model.Airports = this.context.Airports.ToList();
            model.Flights = this.context.Flights.ToList();
            string greeting = string.Empty;
            model.Authenticated = User.Identity.IsAuthenticated;
            if (User.Identity.IsAuthenticated)
            {
                var role = context.Users.Where(x => x.UserName == User.Identity.Name).Select(x => x.Role.Name).FirstOrDefault().ToString();
                ViewData["UserRole"] = role;
                if (role == "Admin")
                {
                    greeting = "Hi, admin, " + User.Identity.Name;
                }
                else
                {
                    greeting = "Hi, " + User.Identity.Name;
                }
            }
            else
            {
                greeting = "<h5 class=\"text-center\"> Welcome to the world of opportunities. </h5> <br /> <br /> <p class=\"display-4\"><a href=\"/Identity/Account/Login\">Login</a> to book a ticket or <br/> <a href=\"/Identity/Account/Register\">Register</a> if you don't have an account</p>";
            }
            model.Greeting = greeting;
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
