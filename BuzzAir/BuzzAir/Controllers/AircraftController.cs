using BuzzAir.Models;
using BuzzAir.Services.Contracts;
using BuzzAir.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Controllers
{
    public class AircraftController : Controller
    {
        private readonly IAircraftService _aircraftService;

        public AircraftController(IAircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public IActionResult CreateAircraft()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Create(AircraftCreateViewModel model)
        {
            var aircraft = await _aircraftService.Create(model.Name, model.Seats);

            return this.Redirect("/");
        }
    }
}