using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Controllers
{
    public class FlightController : Controller
    {
        public IActionResult CreateFlight()
        {
            return View();
        }
    }
}