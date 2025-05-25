namespace BuzzAir.Controllers
{
    public class BoardingPassController(IBoardingPassService boardingPassService) : Controller
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> BoardingPass(string id)
        {
            BoardingPassViewModel viewModel = await boardingPassService.GetBoardingPassAsync(id);

            return View(viewModel);
        }
    }
}