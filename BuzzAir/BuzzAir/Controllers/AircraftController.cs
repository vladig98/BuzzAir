using BuzzAir.Models.CreateModels;
using BuzzAir.Models.DbModels;
using BuzzAir.Models.EditModels;
using BuzzAir.Models.ViewModels;
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

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> All(int? pageNumber)
        {
            int pageSize = 10;

            List<Aircraft> aircraft = await _aircraftService.GetAllAsQueryable(pageSize, pageNumber);
            int count = await _aircraftService.GetCount();

            return View("All", await PaginatedList<Aircraft>.CreateAsync(aircraft, pageNumber ?? 1, pageSize, count));
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Edit(string aircraftId)
        {
            var aircarft = await _aircraftService.GetById(aircraftId);

            if (aircarft == null)
            {
                return BadRequest();
            }

            AircraftEditViewModel model = new AircraftEditViewModel
            {
                Name = aircarft.Name,
                NumbeOfSeats = aircarft.NumberOfSeats,
                Id = aircarft.Id
            };

            return View(model);
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Delete(string aircraftId)
        {
            var exists = await _aircraftService.ExistById(aircraftId);

            if (!exists)
            {
                return BadRequest();
            }

            var aircraft = await _aircraftService.Delete(aircraftId);

            return RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Edit(AircraftEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var exists = await _aircraftService.ExistById(model.Id);

            if (!exists)
            {
                return BadRequest();
            }

            var canChange = await _aircraftService.CanChangeSeats(model.Id, model.NumbeOfSeats);

            if (!canChange)
            {
                return BadRequest();
            }

            var aircraft = await _aircraftService.Edit(model.Id, model.Name, model.NumbeOfSeats);

            return RedirectToAction("All");
        }
    }
}