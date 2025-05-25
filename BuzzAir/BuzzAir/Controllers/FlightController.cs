namespace BuzzAir.Controllers
{
    public class FlightController(IFlightsService flightsService) : Controller
    {
        private const int _pageSize = 10;

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Delete(string flightId)
        {
            await flightsService.Delete(flightId);

            return RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpGet]
        public async Task<IActionResult> CreateFlight()
        {
            CreateFlightViewModel model = await flightsService.CreateViewModel();

            return View(model);
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpGet]
        public async Task<IActionResult> All(int? pageNumber)
        {
            PaginatedList<Flight> paginatedItems = await flightsService.GetAllAsPaginatedListAsync(_pageSize, pageNumber);

            return View("All", paginatedItems);
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Edit(FlightEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await flightsService.Update(model);

            return RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpGet]
        public async Task<IActionResult> Edit(string flightId)
        {
            FlightEditViewModel model = await flightsService.GetEditViewModel(flightId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Create(CreateFlightViewModel model)
        {
            await flightsService.Create(model);

            return this.Redirect("/");
        }
    }
}