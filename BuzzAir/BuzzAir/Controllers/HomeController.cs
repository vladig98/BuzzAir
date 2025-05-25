using System.Diagnostics;

namespace BuzzAir.Controllers
{
    public class HomeController(IFlightsService flightsService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SelectListItem> origins = await flightsService.GetAll();
            IndexViewModel viewModel = HomeFactory.CreateViewModel(origins);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
