namespace BuzzAir.Areas.Admin.Controllers;

[Authorize(Roles = GlobalConstants.ADMIN_ROLE)]
[Area(GlobalConstants.ADMIN_ROLE)]
public class StateController(
    IStateService stateService,
    ICountryService countryService,
    IValidationService validationService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber, CancellationToken token)
    {
        List<StateDTO> states = await stateService.GetAllStatesAsync(pageNumber, 100, token);
        int count = await stateService.CountAsync(token);

        PaginatedList<StateDTO> pagination = new(states, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Deleted(int pageNumber, CancellationToken token)
    {
        List<StateDTO> states = await stateService.GetAllDeletedStatesAsync(pageNumber, 100, token);
        int count = await stateService.CountDeletedAsync(token);

        PaginatedList<StateDTO> pagination = new(states, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken token)
    {
        List<CountryDTO> countries = await countryService.GetAllCountriesAsync(null, null, token);
        CreateStateVM model = StateFactory.BuildCreateStateVM(countries);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStateVM viewModel, CancellationToken token)
    {
        await validationService.ValidateAsync(viewModel, ModelState, token);

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await stateService.AddStateAsync(viewModel, token);
        return Redirect(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id, CancellationToken token)
    {
        StateDTO state = await stateService.GetStateByIdAsync(id, token);

        List<CountryDTO> countries = await countryService.GetAllCountriesAsync(null, null, token);
        EditStateVM model = StateFactory.BuildEditStateVM(state, countries);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditStateVM viewModel, CancellationToken token)
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

        await stateService.UpdateStateAsync(viewModel, token);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id, CancellationToken token)
    {
        StateDTO state = await stateService.GetStateByIdAsync(id, token);
        DeleteStateVM model = StateFactory.BuildDeleteStateVM(state);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteStateVM viewModel, bool hard, CancellationToken token)
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
            await stateService.HardDeleteAsync(viewModel.Id, token);
        }
        else
        {
            await stateService.DeleteAsync(viewModel.Id, token);
        }

        return RedirectToAction(nameof(Deleted));
    }

    [HttpGet]
    public async Task<IActionResult> Restore(string id, CancellationToken token)
    {
        StateDTO state = await stateService.GetDeletedStateByIdAsync(id, token);
        RestoreStateVM model = StateFactory.BuildRestoreStateVM(state);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Restore(RestoreStateVM viewModel, CancellationToken token)
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

        await stateService.RestoreAsync(viewModel.Id, token);
        return RedirectToAction(nameof(Index));
    }
}
