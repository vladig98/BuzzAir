namespace BuzzAir.Areas.Admin.Controllers;

[Authorize(Roles = GlobalConstants.AdminRole)]
[Area(GlobalConstants.AdminRole)]
public class TimezoneController(
    ITimezoneService timezoneService,
    IValidationService validationService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber, CancellationToken token)
    {
        List<TimezoneDTO> timezones = await timezoneService.GetAllTimezonesAsync(pageNumber, 100, token);
        int count = await timezoneService.CountAsync(token);

        PaginatedList<TimezoneDTO> pagination = new(timezones, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public async Task<IActionResult> Deleted(int pageNumber, CancellationToken token)
    {
        List<TimezoneDTO> timezones = await timezoneService.GetAllDeletedTimezonesAsync(pageNumber, 100, token);
        int count = await timezoneService.CountDeletedAsync(token);

        PaginatedList<TimezoneDTO> pagination = new(timezones, count, pageNumber, 100);
        return View(pagination);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTimezoneVM viewModel, CancellationToken token)
    {
        await validationService.ValidateAsync(viewModel, ModelState, token);

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await timezoneService.AddTimezoneAsync(viewModel, token);
        return Redirect(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id, CancellationToken token)
    {
        TimezoneDTO timezone = await timezoneService.GetTimezoneByIdAsync(id, token);
        EditTimezoneVM model = TimezoneFactory.BuildEditTimezoneVM(timezone);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditTimezoneVM viewModel, CancellationToken token)
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

        await timezoneService.UpdateTimezoneAsync(viewModel, token);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id, CancellationToken token)
    {
        TimezoneDTO timezone = await timezoneService.GetTimezoneByIdAsync(id, token);
        DeleteTimezoneVM model = TimezoneFactory.BuildDeleteTimezoneVM(timezone);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteTimezoneVM viewModel, bool hard, CancellationToken token)
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
            await timezoneService.HardDeleteAsync(viewModel.Id, token);
        }
        else
        {
            await timezoneService.DeleteAsync(viewModel.Id, token);
        }

        return RedirectToAction(nameof(Deleted));
    }

    [HttpGet]
    public async Task<IActionResult> Restore(string id, CancellationToken token)
    {
        TimezoneDTO timezone = await timezoneService.GetDeletedTimezoneByIdAsync(id, token);
        RestoreTimezoneVM model = TimezoneFactory.BuildRestoreTimezoneVM(timezone);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Restore(RestoreTimezoneVM viewModel, CancellationToken token)
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

        await timezoneService.RestoreAsync(viewModel.Id, token);
        return RedirectToAction(nameof(Index));
    }
}
