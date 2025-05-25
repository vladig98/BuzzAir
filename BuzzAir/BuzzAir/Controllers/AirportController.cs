namespace BuzzAir.Controllers
{
    public class AirportController(IAirportService airportService) : Controller
    {
        private const int _pageSize = 10;

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> All(int? pageNumber)
        {
            PaginatedList<Airport> airports = await airportService.GetAllAsync(_pageSize, pageNumber);

            return View("All", airports);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> CreateAsync()
        {
            AirportCreateViewModel model = await airportService.GetCreateViewModelAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> CreateAsync(AirportCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await airportService.CreateAsync(model);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> EditAsync(string airportId)
        {
            AirportViewModel model = await airportService.GetEditViewModelAsync(airportId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> EditAsync(AircraftEditViewModel airport)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}