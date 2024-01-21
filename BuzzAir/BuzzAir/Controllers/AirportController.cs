using BuzzAir.Models.CreateModels;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using BuzzAir.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BuzzAir.Controllers
{
    public class AirportController : Controller
    {
        private readonly IAirportService _airportService;
        private readonly ITimezoneService _timezoneService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;

        public AirportController(IAirportService airportService, ITimezoneService timezoneService, ICountryService countryService,
            ICityService cityService, IStateService stateService)
        {
            _airportService = airportService;
            _timezoneService = timezoneService;
            _countryService = countryService;
            _cityService = cityService;
            _stateService = stateService;
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> CreateAirport()
        {
            List<SelectListItem> countries = new List<SelectListItem>();
            List<SelectListItem> timezones = new List<SelectListItem>();

            SelectListGroup countryGroup = new SelectListGroup { Name = "Countries" };
            SelectListGroup dependenciesGroup = new SelectListGroup { Name = "Dependencies and territories not offically recognized as countries" };

            var countryValues = await _countryService.GetAll();

            foreach (Country country in countryValues)
            {
                countries.Add(new SelectListItem()
                {
                    Text = country.Name,
                    Value = country.Id,
                    Group = country.IsCountry ? countryGroup : dependenciesGroup
                });
            }

            var times = await _timezoneService.GetAll();

            List<SelectListGroup> groups = new List<SelectListGroup>()
            {
                new SelectListGroup()
                {
                    Name = "Other"
                }
            };

            foreach (Timezone timezone in times)
            {
                if (timezone.Name.Contains("/"))
                {
                    string groupName = timezone.Name.Split("/")[0];

                    if (!groups.Any(x => x.Name == groupName))
                    {
                        groups.Add(new SelectListGroup { Name = groupName });
                    }
                }

                timezones.Add(new SelectListItem()
                {
                    Text = timezone.Name,
                    Value = timezone.Id,
                    Group = timezone.Name.Contains("/") ? groups.FirstOrDefault(x => x.Name == timezone.Name.Split("/")[0]) : groups.FirstOrDefault(x => x.Name == "Other")
                });
            }

            timezones = timezones.OrderBy(x => x.Group.Name).ThenBy(x => x.Text).ToList();

            var cities = await _cityService.GetAll();
            var states = await _stateService.GetAll();

            AirportCreateViewModel model = new AirportCreateViewModel()
            {
                CountryOptions = countries.OrderBy(x => x.Group.Name).ThenBy(x => x.Text),
                TimezoneOptions = timezones,
                Cities = cities.OrderBy(x => x.Name).ToList(),
                States = states.OrderBy(x => x.Name).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Create(AirportCreateViewModel model)
        {
            var country = await _countryService.GetById(model.Country);
            var city = await _cityService.GetById(model.City);
            var state = await _stateService.GetById(model.State);

            if (country == null)
            {
                return BadRequest();
            }

            var airport = await _airportService.Create(model.ICAO, model.IATA, model.Name, city, state,
                country, model.Elevation, model.Latitude, model.Longitude, model.TimeZone);

            return this.Redirect("/");
        }
    }
}