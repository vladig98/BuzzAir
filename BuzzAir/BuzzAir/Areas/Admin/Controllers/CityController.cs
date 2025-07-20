namespace BuzzAir.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AdminRole)]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class CityController(
        ICityService cityService,
        ICountryService countryService,
        IStateService stateService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            List<SelectListItem> countries = await countryService.GetCountriesForSelect(token);
            List<SelectListItem> states = await stateService.GetStatesForSelectAsync(token);

            CreateCityVM model = CityFactory.InitializeCreateCityViewModel(countries, states);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCityVM model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            model.Country = await countryService.GetByIdAsync(model.CountryId, token);
            model.State = await stateService.GetByIdAsync(model.StateId, token);

            await cityService.CreateAsync(model, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, CancellationToken token)
        {
            EditCityVM model = await cityService.GetEditDetailsAsync(id, token);
            List<SelectListItem> countries = await countryService.GetCountriesForSelect(token);
            List<SelectListItem> states = await stateService.GetStatesForSelectAsync(token);

            CityFactory.UpdateEditViewModelWithSelects(model, countries, states);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCityVM model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            model.Country = await countryService.GetByIdAsync(model.CountryId, token);
            model.State = await stateService.GetByIdAsync(model.StateId, token);

            await cityService.EditAsync(model, token);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, CancellationToken token)
        {
            DeleteCityVM model = await cityService.GetDeleteDetailsAsync(id, token);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string id, CancellationToken token)
        {
            await cityService.DeleteAsync(id, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Restore(string id, CancellationToken token)
        {
            RestoreCityVM model = await cityService.GetRestoreDetailsAsync(id, token);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestorePost(string id, CancellationToken token)
        {
            await cityService.RestoreAsync(id, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All(int? pageNumber, CancellationToken token)
        {
            PaginatedList<CityDTO> cities = await cityService.AllAsync(pageNumber, token);

            return View(cities);
        }

        [HttpGet]
        public async Task<IActionResult> AllDeleted(int? pageNumber, CancellationToken token)
        {
            PaginatedList<CityDTO> cities = await cityService.AllDeletedAsync(pageNumber, token);

            return View(cities);
        }
    }
}
