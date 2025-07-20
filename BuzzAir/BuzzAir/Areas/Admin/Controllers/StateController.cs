namespace BuzzAir.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AdminRole)]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class StateController(
        IStateService stateService,
        ICountryService countryService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            List<SelectListItem> countries = await countryService.GetCountriesForSelect(token);
            CreateStateVM model = StateFactory.InitializeCreateCityViewModel(countries);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStateVM model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            model.Country = await countryService.GetByIdAsync(model.CountryId, token);

            await stateService.CreateAsync(model, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, CancellationToken token)
        {
            EditStateVM model = await stateService.GetEditDetailsAsync(id, token);
            List<SelectListItem> countries = await countryService.GetCountriesForSelect(token);

            StateFactory.UpdateEditViewModelWithSelects(model, countries);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditStateVM model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            model.Country = await countryService.GetByIdAsync(model.CountryId, token);
            await stateService.EditAsync(model, token);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, CancellationToken token)
        {
            DeleteStateVM model = await stateService.GetDeleteDetailsAsync(id, token);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string id, CancellationToken token)
        {
            await stateService.DeleteAsync(id, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Restore(string id, CancellationToken token)
        {
            RestoreStateVM model = await stateService.GetRestoreDetailsAsync(id, token);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestorePost(string id, CancellationToken token)
        {
            await stateService.RestoreAsync(id, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All(int? pageNumber, CancellationToken token)
        {
            PaginatedList<StateDTO> states = await stateService.AllAsync(pageNumber, token);

            return View(states);
        }

        [HttpGet]
        public async Task<IActionResult> AllDeleted(int? pageNumber, CancellationToken token)
        {
            PaginatedList<StateDTO> states = await stateService.AllDeletedAsync(pageNumber, token);

            return View(states);
        }
    }
}
