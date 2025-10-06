namespace BuzzAir.Areas.Admin.Controllers;

[Authorize(Roles = GlobalConstants.ADMIN_ROLE)]
[Area(GlobalConstants.ADMIN_ROLE)]
public class AircraftController(
    IAircraftService aircraftService,
    IValidationService validationService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber, CancellationToken token)
    {
        List<AircraftDTO> aircraft = await aircraftService.GetAllAircraftAsync(pageNumber, 100, token);
        int count = await aircraftService.CountAsync(token);

        PaginatedList<AircraftDTO> pagination = new(aircraft, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Deleted(int pageNumber, CancellationToken token)
    {
        List<AircraftDTO> aircraft = await aircraftService.GetAllDeletedAircraftAsync(pageNumber, 100, token);
        int count = await aircraftService.CountDeletedAsync(token);

        PaginatedList<AircraftDTO> pagination = new(aircraft, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAircraftVM viewModel, CancellationToken token)
    {
        await validationService.ValidateAsync(viewModel, ModelState, token);

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await aircraftService.AddAircraftAsync(viewModel, token);
        return Redirect(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id, CancellationToken token)
    {
        AircraftDTO aircraft = await aircraftService.GetAircraftByIdAsync(id, token);
        EditAircraftVM model = AircraftFactory.BuildEditAircraftVM(aircraft);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditAircraftVM viewModel, CancellationToken token)
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

        await aircraftService.UpdateAircraftAsync(viewModel, token);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id, CancellationToken token)
    {
        AircraftDTO aircraft = await aircraftService.GetAircraftByIdAsync(id, token);
        DeleteAircraftVM model = AircraftFactory.BuildDeleteAircraftVM(aircraft);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteAircraftVM viewModel, bool hard, CancellationToken token)
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
            await aircraftService.HardDeleteAsync(viewModel.Id, token);
        }
        else
        {
            await aircraftService.DeleteAsync(viewModel.Id, token);
        }

        return RedirectToAction(nameof(Deleted));
    }

    [HttpGet]
    public async Task<IActionResult> Restore(string id, CancellationToken token)
    {
        AircraftDTO aircraft = await aircraftService.GetDeletedAircraftByIdAsync(id, token);
        RestoreAircraftVM model = AircraftFactory.BuildRestoreAircraftVM(aircraft);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Restore(RestoreAircraftVM viewModel, CancellationToken token)
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

        await aircraftService.RestoreAsync(viewModel.Id, token);
        return RedirectToAction(nameof(Index));
    }
}
