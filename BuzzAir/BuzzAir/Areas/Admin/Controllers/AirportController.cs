namespace BuzzAir.Areas.Admin.Controllers;

[Authorize(Roles = GlobalConstants.ADMIN_ROLE)]
[Area(GlobalConstants.ADMIN_ROLE)]
public class AirportController(
    IAirportService airportService,
    ICityService cityService,
    IValidationService validationService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber, CancellationToken token)
    {
        List<AirportDTO> airports = await airportService.GetAllAirportsAsync(pageNumber, 100, token);
        int count = await airportService.CountAsync(token);

        PaginatedList<AirportDTO> pagination = new(airports, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Deleted(int pageNumber, CancellationToken token)
    {
        List<AirportDTO> airports = await airportService.GetAllDeletedAirportsAsync(pageNumber, 100, token);
        int count = await airportService.CountDeletedAsync(token);

        PaginatedList<AirportDTO> pagination = new(airports, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken token)
    {
        List<CityDTO> cities = await cityService.GetAllCitiiesAsync(null, null, token);
        CreateAirportVM model = AirportFactory.BuildCreateAirportVM(cities);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAirportVM viewModel, CancellationToken token)
    {
        await validationService.ValidateAsync(viewModel, ModelState, token);

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await airportService.AddAirportAsync(viewModel, token);
        return Redirect(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id, CancellationToken token)
    {
        AirportDTO airport = await airportService.GetAirportByIdAsync(id, token);
        List<CityDTO> cities = await cityService.GetAllCitiiesAsync(null, null, token);

        EditAirportVM model = AirportFactory.BuildEditAirportVM(airport, cities);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditAirportVM viewModel, CancellationToken token)
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

        await airportService.UpdateAirportAsync(viewModel, token);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id, CancellationToken token)
    {
        AirportDTO airport = await airportService.GetAirportByIdAsync(id, token);
        DeleteAirportVM model = AirportFactory.BuildDeleteAirportVM(airport);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteAirportVM viewModel, bool hard, CancellationToken token)
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
            await airportService.HardDeleteAsync(viewModel.Id, token);
        }
        else
        {
            await airportService.DeleteAsync(viewModel.Id, token);
        }

        return RedirectToAction(nameof(Deleted));
    }

    [HttpGet]
    public async Task<IActionResult> Restore(string id, CancellationToken token)
    {
        AirportDTO airport = await airportService.GetDeletedAirportByIdAsync(id, token);
        RestoreAirportVM model = AirportFactory.BuildRestoreAirportVM(airport);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Restore(RestoreAirportVM viewModel, CancellationToken token)
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

        await airportService.RestoreAsync(viewModel.Id, token);
        return RedirectToAction(nameof(Index));
    }
}
