namespace BuzzAir.Controllers
{
    public class AircraftController(IAircraftService aircraftService) : Controller
    {
        private const int _pageSize = 10;

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> AllAsync(int? pageNumber)
        {
            PaginatedList<Aircraft> paginatedItems = await aircraftService.GetAllAsPaginatedListAsync(_pageSize, pageNumber);

            return View("All", paginatedItems);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public IActionResult CreateAsync()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> CreateAsync(AircraftCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await aircraftService.CreateAsync(model.Name, model.Seats);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> EditAsync(string aircraftId)
        {
            AircraftEditViewModel model = await aircraftService.GetEditModelAsync(aircraftId);

            return View(model);
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> EditAsync(AircraftEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await aircraftService.EditAsync(model.Id, model.Name, model.NumbeOfSeats);

            return RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string aircraftId)
        {
            await aircraftService.DeleteAsync(aircraftId);

            return RedirectToAction("All");
        }
    }
}