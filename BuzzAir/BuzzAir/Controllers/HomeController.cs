using BuzzAir.Models;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace BuzzAir.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFlightsService _flightsService;

        public HomeController(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        public async Task<IActionResult> Index()
        {
            var flights = await _flightsService.GetAll();
            List<SelectListItem> origins = new List<SelectListItem>();

            List<SelectListGroup> groups = new List<SelectListGroup>();
            List<string> names = new List<string>();

            foreach (Flight flight in flights)
            {
                if (!groups.Any(x => x.Name == flight.Origin.Country.Name))
                {
                    groups.Add(new SelectListGroup { Name = flight.Origin.Country.Name });
                }

                if (!names.Any(x => x == flight.Origin.City.Name))
                {
                    names.Add(flight.Origin.City.Name);

                    origins.Add(new SelectListItem
                    {
                        Text = flight.Origin.City.Name,
                        Value = flight.Origin.City.Id,
                        Group = groups.First(x => x.Name == flight.Origin.Country.Name)
                    });
                }
            }

            IndexViewModel model = new IndexViewModel()
            {
                Origins = origins.OrderBy(x => x.Group.Name).ThenBy(x => x.Text)
            };

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
