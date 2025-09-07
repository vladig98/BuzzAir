namespace BuzzAir.Controllers;

public sealed class HomeController(IFlightService flightService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken token)
    {
        List<FlightDTO> flights = await flightService.GetAllFlightsAsync(1, 100, token);
        return View(flights);
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
    public IActionResult Error()
    {
        return View();
    }
}
