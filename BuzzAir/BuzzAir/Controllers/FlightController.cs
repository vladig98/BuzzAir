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
        [HttpPost]
        public async Task<IActionResult> Delete(string flightId)
        {
            bool flightExists = await _flightsService.Exists(flightId);

            if (!flightExists)
            {
                return BadRequest();
            }

            await _flightsService.Delete(flightId);

            return RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> CreateFlight()
        {
            var aircaft = await GetAircraft();
            var countries = await GetCountries();

            CreateFlightViewModel model = new CreateFlightViewModel()
            {
                Aircrafts = aircaft,
                OriginCountryOptions = countries,
                DestinationCountryOptions = new List<SelectListItem>(countries)
            };

            return View(model);
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> All(int? pageNumber)
        {
            int pageSize = 10;

            List<Flight> flights = await _flightsService.GetAllAsQueryable(pageSize, pageNumber);
            int count = await _flightsService.GetCount();

            return View("All", await PaginatedList<Flight>.CreateAsync(flights, pageNumber ?? 1, pageSize, count));
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost]
        public async Task<IActionResult> Edit(FlightEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _flightsService.Update(model);
            }

            return RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Edit(string flightId)
        {
            var flight = await _flightsService.GetById(flightId);

            if (flight == null)
            {
                return BadRequest();
            }

            var aircraft = await GetAircraft();
            var countries = await GetCountries();

            var airportsOrigin = await _airportService.GetAllForCountry(flight.Origin.CountryId);
            var airportsDestination = await _airportService.GetAllForCountry(flight.Destination.CountryId);

            List<SelectListItem> airportsO = new List<SelectListItem>();

            foreach (var airport in airportsOrigin)
            {
                airportsO.Add(new SelectListItem
                {
                    Text = airport.Name,
                    Value = airport.Id
                });
            }

            List<SelectListItem> airportsD = new List<SelectListItem>();

            foreach (var airport in airportsDestination)
            {
                airportsD.Add(new SelectListItem
                {
                    Text = airport.Name,
                    Value = airport.Id
                });
            }

            FlightEditViewModel model = new FlightEditViewModel
            {
                Departure = flight.Departure,
                Destination = flight.DestinationId,
                DestinationCountry = flight.Destination.CountryId,
                FlightNumber = flight.FlightNumber,
                Origin = flight.OriginId,
                OriginCountry = flight.Origin.CountryId,
                Price = flight.Price,
                Aircraft = flight.AircraftId,
                DurationInMinutes = flight.DurationInMinutes,
                AircraftOptions = aircraft,
                OriginOptions = countries,
                DestinationOptions = new List<SelectListItem>(countries),
                DestinationAirportsOptions = airportsD,
                OriginAirportsOptions = airportsO,
                Id = flight.Id
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Create(CreateFlightViewModel model)
        {
            var origin = await _airportService.GetByIdAsync(model.Origin);
            var destination = await _airportService.GetByIdAsync(model.Destination);

            if (origin == null || destination == null)
            {
                model.Aircrafts = await GetAircraft();

                var countries = await GetCountries();

                model.OriginCountryOptions = countries;
                model.DestinationCountryOptions = new List<SelectListItem>(countries);

                return View("CreateFlight", model);
            }

            var aircraft = await _aircraftService.GetByIdAsync(model.Aircraft);

            if (aircraft == null)
            {
                model.Aircrafts = await GetAircraft();

                var countries = await GetCountries();

                model.OriginCountryOptions = countries;
                model.DestinationCountryOptions = new List<SelectListItem>(countries);

                return View("CreateFlight", model);
            }

            if (model.Departure < DateTime.Now)
            {
                model.Aircrafts = await GetAircraft();

                var countries = await GetCountries();

                model.OriginCountryOptions = countries;
                model.DestinationCountryOptions = new List<SelectListItem>(countries);

                return View("CreateFlight", model);
            }

            DateTime arrival = model.Departure.AddMinutes(model.DurationInMinutes);

            var flight = await _flightsService.Create(aircraft, model.DurationInMinutes, model.Price, model.Departure, arrival, model.FlightNumber, origin, destination);

            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        private async Task<List<SelectListItem>> GetAircraft()
        {
            var aircafts = await _aircraftService.GetAllAsync();
            List<SelectListItem> aircraftSelect = new List<SelectListItem>();
            foreach (Aircraft a in aircafts)
            {
                aircraftSelect.Add(new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id
                });
            }

            return aircraftSelect;
        }

        [Authorize(Roles = GlobalConstants.AdminRole)]
        private async Task<List<SelectListItem>> GetCountries()
        {
            List<SelectListItem> countries = new List<SelectListItem>();

            SelectListGroup countryGroup = new SelectListGroup { Name = "Countries" };
            SelectListGroup dependenciesGroup = new SelectListGroup { Name = "Dependencies and territories not offically recognized as countries" };

            var countryValues = await _countryService.GetAllAsync();

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

            return countries;
        }
    }
}