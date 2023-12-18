using BuzzAir.Models;
using BuzzAir.Models.DbModels;
using BuzzAir.Services.Contracts;
using BuzzAir.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BuzzAir.Controllers
{
    public class FlightController : Controller
    {
        private readonly IAircraftService _aircraftService;
        private readonly IAirportService _airportService;
        private readonly IFlightsService _flightsService;
        private readonly ICountryService _countryService;

        public FlightController(IAircraftService aircraftService, IAirportService airportService, IFlightsService flightsService, ICountryService countryService)
        {
            _aircraftService = aircraftService;
            _airportService = airportService;
            _flightsService = flightsService;
            _countryService = countryService;
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> CreateFlight()
        {
            var aircafts = await _aircraftService.GetAll();
            var airports = await _airportService.GetAll();

            List<SelectListItem> aircraftSelect = new List<SelectListItem>();
            List<SelectListItem> airportsSelect = new List<SelectListItem>();

            foreach (Aircraft aircraft in aircafts)
            {
                aircraftSelect.Add(new SelectListItem
                {
                    Text = aircraft.Name,
                    Value = aircraft.Id
                });
            }

            foreach (Airport airport in airports)
            {
                airportsSelect.Add(new SelectListItem
                {
                    Text = airport.Name,
                    Value = airport.Id
                });
            }

            List<SelectListItem> countries = new List<SelectListItem>();

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

            countries = countries.OrderBy(x => x.Group.Name).ThenBy(x => x.Text).ToList();

            CreateFlightViewModel model = new CreateFlightViewModel()
            {
                Aircrafts = aircraftSelect,
                Airports = airportsSelect,
                OriginCountryOptions = countries,
                DestinationCountryOptions = new List<SelectListItem>(countries)
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Create(CreateFlightViewModel model)
        {
            var origin = await _airportService.GetById(model.Origin);
            var destination = await _airportService.GetById(model.Destination);

            if (origin == null || destination == null)
            {
                return BadRequest();
            }

            var aircraft = await _aircraftService.GetById(model.Aircraft);

            if (aircraft == null)
            {
                return BadRequest();
            }

            DateTime arrival = model.Departure.AddMinutes(model.DurationInMinutes);

            var flight = await _flightsService.Create(aircraft, model.DurationInMinutes, model.Price, model.Departure, arrival, model.FlightNumber, origin, destination);

            return this.Redirect("/");
        }
    }
}