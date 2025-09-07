namespace BuzzAir.Areas.Admin.Controllers;

[Authorize(Roles = GlobalConstants.AdminRole)]
[Area(GlobalConstants.AdminRole)]
public class FlightController(
    IFlightService flightService,
    IAirportService airportService,
    IAircraftService aircraftService,
    IValidationService validationService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber, CancellationToken token)
    {
        List<FlightDTO> flights = await flightService.GetAllFlightsAsync(pageNumber, 100, token);
        int count = await flightService.GetCountAsync(token);

        PaginatedList<FlightDTO> pagination = new(flights, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Deleted(int pageNumber, CancellationToken token)
    {
        List<FlightDTO> flights = await flightService.GetAllDeletedFlightsAsync(pageNumber, 100, token);
        int count = await flightService.GetDeletedCountAsync(token);

        PaginatedList<FlightDTO> pagination = new(flights, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken token)
    {
        List<AirportDTO> airports = await airportService.GetAllAirportsAsync(null, null, token);
        List<AircraftDTO> aircraft = await aircraftService.GetAllAircraftAsync(null, null, token);

        CreateFlightVM model = FlightFactory.BuildCreateFlightVM(airports, aircraft);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFlightVM viewModel, CancellationToken token)
    {
        await validationService.ValidateAsync(viewModel, ModelState, token);

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await flightService.AddFlightAsync(viewModel, token);
        return Redirect(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id, CancellationToken token)
    {
        FlightDTO flight = await flightService.GetFlightByIdAsync(id, token);

        List<AirportDTO> airports = await airportService.GetAllAirportsAsync(null, null, token);
        List<AircraftDTO> aircraft = await aircraftService.GetAllAircraftAsync(null, null, token);

        EditFlightVM model = FlightFactory.BuildEditFlightVM(flight, airports, aircraft);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditFlightVM viewModel, CancellationToken token)
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

        await flightService.UpdateFlightAsync(viewModel, token);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id, CancellationToken token)
    {
        FlightDTO flight = await flightService.GetFlightByIdAsync(id, token);
        DeleteFlightVM model = FlightFactory.BuildDeleteFlightVM(flight);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteFlightVM viewModel, bool hard, CancellationToken token)
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
            await flightService.HardDeleteAsync(viewModel.Id, token);
        }
        else
        {
            await flightService.DeleteAsync(viewModel.Id, token);
        }

        return RedirectToAction(nameof(Deleted));
    }

    [HttpGet]
    public async Task<IActionResult> Restore(string id, CancellationToken token)
    {
        FlightDTO flight = await flightService.GetDeletedFlightByIdAsync(id, token);
        RestoreFlightVM model = FlightFactory.BuildRestoreFlightVM(flight);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Restore(RestoreFlightVM viewModel, CancellationToken token)
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

        await flightService.RestoreAsync(viewModel.Id, token);
        return RedirectToAction(nameof(Index));
    }
}
