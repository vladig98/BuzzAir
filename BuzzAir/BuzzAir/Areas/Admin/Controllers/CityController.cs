namespace BuzzAir.Areas.Admin.Controllers;

[Area(GlobalConstants.AdminRole)]
public class CityController(
    ICountryService countryService,
    ITimezoneService timezoneService,
    IStateService stateService,
    ICityService cityService,
    IValidationService validationService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber, CancellationToken token)
    {
        List<CityDTO> cities = await cityService.GetAllCitiiesAsync(pageNumber, 100, token);
        int count = await cityService.GetCountAsync(token);

        PaginatedList<CityDTO> pagination = new(cities, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Deleted(int pageNumber, CancellationToken token)
    {
        List<CityDTO> cities = await cityService.GetAllDeletedCitiiesAsync(pageNumber, 100, token);
        int count = await cityService.GetDeletedCountAsync(token);

        PaginatedList<CityDTO> pagination = new(cities, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken token)
    {
        List<CountryDTO> countries = await countryService.GetAllCountriesAsync(null, null, token);
        List<TimezoneDTO> timezones = await timezoneService.GetTimezonesAsync(token);

        CreateCityVM model = CityFactory.BuildCreateCityVM(countries, timezones);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCityVM viewModel, CancellationToken token)
    {
        await validationService.ValidateAsync(viewModel, ModelState, token);

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await cityService.AddCityAsync(viewModel, token);

        return Redirect(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id, CancellationToken token)
    {
        CityDTO city = await cityService.GetCityByIdAsync(id, token);
        string countryId = await countryService.GetIdByNameAsync(city.Country, token);

        List<CountryDTO> countries = await countryService.GetAllCountriesAsync(null, null, token);
        List<StateDTO> states = await stateService.GetStatesByCountryAsync(countryId, token);
        List<TimezoneDTO> timezones = await timezoneService.GetTimezonesAsync(token);

        EditCityVM model = CityFactory.BuildEditCityVM(city, countries, states, timezones);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditCityVM viewModel, CancellationToken token)
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

        await cityService.UpdateCityAsync(viewModel, token);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id, CancellationToken token)
    {
        CityDTO city = await cityService.GetCityByIdAsync(id, token);
        DeleteCityVM model = CityFactory.BuildDeleteCityVM(city);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteCityVM viewModel, bool hard, CancellationToken token)
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
            await cityService.HardDeleteAsync(viewModel.Id, token);
        }
        else
        {
            await cityService.DeleteAsync(viewModel.Id, token);
        }

        return RedirectToAction(nameof(Deleted));
    }

    [HttpGet]
    public async Task<IActionResult> Restore(string id, CancellationToken token)
    {
        CityDTO city = await cityService.GetDeletedCityByIdAsync(id, token);
        RestoreCityVM model = CityFactory.BuildRestoreCityVM(city);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Restore(RestoreCityVM viewModel, CancellationToken token)
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

        await cityService.RestoreAsync(viewModel.Id, token);
        return RedirectToAction(nameof(Index));
    }
}