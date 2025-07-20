using BuzzAir.Areas.Admin.ViewModels.AircraftDTOs;

namespace BuzzAir.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AdminRole)]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class AircraftController(IAircraftService aircraftService) : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAircraftVM model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await aircraftService.CreateAsync(model, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, CancellationToken token)
        {
            EditAircraftVM model = await aircraftService.GetEditDetailsAsync(id, token);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAircraftVM model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await aircraftService.EditAsync(model, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, CancellationToken token)
        {
            DeleteAircraftVM model = await aircraftService.GetDeleteDetailsAsync(id, token);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string id, CancellationToken token)
        {
            await aircraftService.DeleteAsync(id, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Restore(string id, CancellationToken token)
        {
            RestoreAircraftVM model = await aircraftService.GetRestoreDetailsAsync(id, token);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestorePost(string id, CancellationToken token)
        {
            await aircraftService.RestoreAsync(id, token);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All(int? pageNumber, CancellationToken token)
        {
            PaginatedList<AircraftDTO> aircraft = await aircraftService.AllAsync(pageNumber, token);
            return View(aircraft);
        }

        [HttpGet]
        public async Task<IActionResult> AllDeleted(int? pageNumber, CancellationToken token)
        {
            PaginatedList<AircraftDTO> aircraft = await aircraftService.AllDeletedAsync(pageNumber, token);
            return View(aircraft);
        }
    }
}
