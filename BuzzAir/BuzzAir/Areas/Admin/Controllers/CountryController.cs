namespace BuzzAir.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AdminRole)]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class CountryController(ICountryService countryService) : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCountryVM model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await countryService.CreateAsync(model, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, CancellationToken token)
        {
            EditCountryVM model = await countryService.GetEditDetailsAsync(id, token);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCountryVM model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await countryService.EditAsync(model, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, CancellationToken token)
        {
            DeleteCountryVM model = await countryService.GetDeleteDetailsAsync(id, token);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string id, CancellationToken token)
        {
            await countryService.DeleteAsync(id, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Restore(string id, CancellationToken token)
        {
            RestoreCountryVM model = await countryService.GetRestoreDetailsAsync(id, token);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestorePost(string id, CancellationToken token)
        {
            await countryService.RestoreAsync(id, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All(int? pageNumber, CancellationToken token)
        {
            PaginatedList<CountryDTO> countries = await countryService.AllAsync(pageNumber, token);
            return View(countries);
        }

        [HttpGet]
        public async Task<IActionResult> AllDeleted(int? pageNumber, CancellationToken token)
        {
            PaginatedList<CountryDTO> countries = await countryService.AllDeletedAsync(pageNumber, token);
            return View(countries);
        }
    }
}
