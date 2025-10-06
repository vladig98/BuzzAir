namespace BuzzAir.Areas.Admin.Controllers;

[Authorize(Roles = GlobalConstants.ADMIN_ROLE)]
[Area(GlobalConstants.ADMIN_ROLE)]
public class CountryController(
    ICountryService countryService,
    IValidationService validationService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber, CancellationToken token)
    {
        List<CountryDTO> countries = await countryService.GetAllCountriesAsync(pageNumber, 100, token);
        int count = await countryService.GetCountAsync(token);

        PaginatedList<CountryDTO> pagination = new(countries, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Deleted(int pageNumber, CancellationToken token)
    {
        List<CountryDTO> countries = await countryService.GetAllDeletedCountriesAsync(pageNumber, 100, token);
        int count = await countryService.GetDeletedCountAsync(token);

        PaginatedList<CountryDTO> pagination = new(countries, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCountryVM viewModel, CancellationToken token)
    {
        await validationService.ValidateAsync(viewModel, ModelState, token);

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await countryService.AddCountryAsync(viewModel, token);
        return Redirect(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id, CancellationToken token)
    {
        CountryDTO country = await countryService.GetCountryByIdAsync(id, token);
        EditCountryVM model = CountryFactory.BuildEditCountryVM(country);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditCountryVM viewModel, CancellationToken token)
    {
        if (viewModel is null)
        {
            return BadRequest();
        }

        await validationService.ValidateAsync(viewModel, ModelState, token);
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await countryService.UpdateCountryAsync(viewModel, token);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id, CancellationToken token)
    {
        CountryDTO country = await countryService.GetCountryByIdAsync(id, token);
        DeleteCountryVM model = CountryFactory.BuildDeleteCountryVM(country);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteCountryVM viewModel, bool hard, CancellationToken token)
    {
        if (viewModel is null)
        {
            return BadRequest();
        }

        await validationService.ValidateAsync(viewModel, ModelState, token);
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        if (hard)
        {
            await countryService.HardDeleteAsync(viewModel.Id, token);
        }
        else
        {
            await countryService.DeleteAsync(viewModel.Id, token);
        }

        return RedirectToAction(nameof(Deleted));
    }

    [HttpGet]
    public async Task<IActionResult> Restore(string id, CancellationToken token)
    {
        CountryDTO country = await countryService.GetDeletedCountryByIdAsync(id, token);
        RestoreCountryVM model = CountryFactory.BuildRestoreCountryVM(country);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Restore(RestoreCountryVM viewModel, CancellationToken token)
    {
        if (viewModel is null)
        {
            return BadRequest();
        }

        await validationService.ValidateAsync(viewModel, ModelState, token);
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await countryService.RestoreAsync(viewModel.Id, token);
        return RedirectToAction(nameof(Index));
    }
}
