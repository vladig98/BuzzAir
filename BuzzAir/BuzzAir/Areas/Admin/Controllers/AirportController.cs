namespace BuzzAir.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AdminRole)]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class AirportController(
        IAirportService airportService,
        ICountryService countryService,
        ICityService cityService,
        IStateService stateService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            List<SelectListItem> countries = await countryService.GetCountriesForSelect(token);
            CreateAirportVM model = AirportFactory.InitializeCreateAirportViewModel(countries);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAirportVM model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            model.Country = await countryService.GetByIdAsync(model.CountryId, token);
            model.State = await stateService.GetByIdAsync(model.StateId, token);
            model.City = await cityService.GetByIdAsync(model.CityId, token);
            
            await airportService.CreateAsync(model, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, CancellationToken token)
        {
            EditAirportVM model = await airportService.GetEditDetailsAsync(id, token);
            List<SelectListItem> countries = await countryService.GetCountriesForSelect(token);

            AirportFactory.UpdateEditViewModelWithSelects(model, countries);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAirportVM model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            model.Country = await countryService.GetByIdAsync(model.CountryId, token);
            model.State = await stateService.GetByIdAsync(model.StateId, token);
            model.City = await cityService.GetByIdAsync(model.CityId, token);

            await airportService.EditAsync(model, token);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, CancellationToken token)
        {
            DeleteAirportVM model = await airportService.GetDeleteDetailsAsync(id, token);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string id, CancellationToken token)
        {
            await airportService.DeleteAsync(id, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Restore(string id, CancellationToken token)
        {
            RestoreAirportVM model = await airportService.GetRestoreDetailsAsync(id, token);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestorePost(string id, CancellationToken token)
        {
            await airportService.RestoreAsync(id, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All(int? pageNumber, CancellationToken token)
        {
            PaginatedList<AirportDTO> airports = await airportService.AllAsync(pageNumber, token);

            return View(airports);
        }

        [HttpGet]
        public async Task<IActionResult> AllDeleted(int? pageNumber, CancellationToken token)
        {
            PaginatedList<AirportDTO> airports = await airportService.AllDeletedAsync(pageNumber, token);

            return View(airports);
        }
    }
}
