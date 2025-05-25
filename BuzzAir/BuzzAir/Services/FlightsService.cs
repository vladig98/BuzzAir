namespace BuzzAir.Services
{
    public class FlightsService(
        BuzzAirDbContext context,
        IAircraftService aircraftService,
        ICountryService countryService,
        IAirportService airportService,
        ISeatService seatService) : IFlightsService
    {
        public async Task Create(CreateFlightViewModel model)
        {
            Airport origin = await airportService.GetByIdAsync(model.Origin);
            Airport destination = await airportService.GetByIdAsync(model.Destination);

            if (origin == null || destination == null)
            {
                throw new ArgumentException($"You need to pick origin and destination to create a flight.");
            }

            Aircraft aircraft = await aircraftService.GetByIdAsync(model.Aircraft);

            if (model.Departure < DateTime.UtcNow)
            {
                throw new ArgumentException($"The chosen date for the flight is in the past.");
            }

            Flight flight = FlightFactory.Create(origin, destination, aircraft, model);

            await seatService.CreateSeats(flight, aircraft.NumberOfSeats);
            await context.Flights.AddAsync(flight);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string flightId)
        {
            Flight flight = await context.Flights.FirstOrDefaultAsync(x => x.Id == flightId) ??
                throw new ArgumentException($"Invalid flight with id {flightId}");

            flight.IsDeleted = true;

            await context.SaveChangesAsync();
        }

        public async Task<List<SelectListItem>> GetAll()
        {
            List<Flight> flights = await context.Flights
                .Where(x => !x.IsDeleted)
                .Include(x => x.Origin)
                    .ThenInclude(x => x.City)
                .Include(x => x.Origin)
                    .ThenInclude(x => x.Country)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            List<SelectListItem> flightSelect = FlightFactory.GetFlightsForSelect(flights);

            return flightSelect;
        }

        public async Task<IEnumerable<Flight>> GetFlightsByOriginAndDestination(City origin, City destination, DateTime departure)
        {
            return await context.Flights.Where(x => !x.IsDeleted)
                .Where(x => x.Origin.City.Name == origin.Name)
                .Where(x => x.Destination.City.Name == destination.Name)
                .Where(x => x.Departure.Year == departure.Year && x.Departure.Month == departure.Month && x.Departure.Day == departure.Day)
                .Include(x => x.Destination).ThenInclude(x => x.City)
                .Include(x => x.Destination).ThenInclude(x => x.State)
                .Include(x => x.Destination).ThenInclude(x => x.Country)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.State)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .AsSplitQuery().ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsByCityId(string cityId)
        {
            return await context.Flights.Where(x => !x.IsDeleted)
                .Where(x => x.Origin.City.Id == cityId)
                .Include(x => x.Destination).ThenInclude(x => x.City)
                .Include(x => x.Destination).ThenInclude(x => x.State)
                .Include(x => x.Destination).ThenInclude(x => x.Country)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.State)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .AsSplitQuery().ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsForOriginIdAndDestinationId(string originId, string destinationId)
        {
            return await context.Flights.Where(x => !x.IsDeleted)
                .Where(x => x.Origin.City.Id == originId)
                .Where(x => x.Destination.City.Id == destinationId)
                .Where(x => x.Departure > DateTime.Now)
                .Include(x => x.Destination).ThenInclude(x => x.City)
                .Include(x => x.Destination).ThenInclude(x => x.State)
                .Include(x => x.Destination).ThenInclude(x => x.Country)
                .Include(x => x.Origin).ThenInclude(x => x.City)
                .Include(x => x.Origin).ThenInclude(x => x.State)
                .Include(x => x.Origin).ThenInclude(x => x.Country)
                .AsSplitQuery().ToListAsync();
        }

        public async Task<Flight?> GetById(string id)
        {
            return await context.Flights
                .Where(x => !x.IsDeleted)
                .Where(x => x.Id == id)
                .Include(x => x.Destination)
                    .ThenInclude(x => x.City)
                .Include(x => x.Destination)
                    .ThenInclude(x => x.State)
                .Include(x => x.Destination)
                    .ThenInclude(x => x.Country)
                .Include(x => x.Origin)
                    .ThenInclude(x => x.City)
                .Include(x => x.Origin)
                    .ThenInclude(x => x.State)
                .Include(x => x.Origin)
                    .ThenInclude(x => x.Country)
                .Include(x => x.Aircraft)
                .Include(x => x.Seats)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

        public async Task<List<Flight>> GetAllAsync(int pageSize, int pageNumber)
        {
            int toSkip = (pageNumber - 1) * pageSize;

            return await context.Flights
                .Where(x => !x.IsDeleted)
                .Include(x => x.Origin)
                    .ThenInclude(x => x.City)
                .Include(x => x.Origin)
                    .ThenInclude(x => x.Country)
                .Include(x => x.Destination)
                    .ThenInclude(x => x.City)
                .Include(x => x.Destination)
                    .ThenInclude(x => x.Country)
                .Include(x => x.Aircraft)
                .Where(x => x.Departure > DateTime.Now)
                .OrderBy(x => x.FlightNumber)
                .Skip(toSkip)
                .Take(pageSize)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await context.Flights.Where(x => !x.IsDeleted).Where(x => x.Departure > DateTime.Now).CountAsync();
        }

        public async Task Update(FlightEditViewModel model)
        {
            Flight? flight = await GetById(model.Id) ??
                throw new ArgumentException($"Can't find a flight with id {model.Id}");

            flight.FlightNumber = model.FlightNumber;
            flight.OriginId = model.Origin;
            flight.DestinationId = model.Destination;
            flight.DurationInMinutes = model.DurationInMinutes;
            flight.Departure = model.Departure;
            flight.Arrival = model.Departure.AddMinutes(model.DurationInMinutes);
            flight.Price = model.Price;
            flight.AircraftId = model.Aircraft;

            await context.SaveChangesAsync();
        }

        public List<FlightViewModel> GetFlightsDetails(ICollection<BookingFlight> flights)
        {
            int count = flights.Count;
            List<FlightViewModel> viewModels = new(count);

            foreach (BookingFlight flight in flights)
            {
                FlightViewModel flightViewModel = FlightFactory.CreateViewModel(flight);

                viewModels.Add(flightViewModel);
            }

            return viewModels;
        }

        public List<FlightViewModel> GetViewModels(IEnumerable<Flight> flights)
        {
            List<FlightViewModel> viewModels = [];

            foreach (Flight flight in flights)
            {
                FlightViewModel viewModel = FlightFactory.CreateViewModel(flight);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public async Task<CreateFlightViewModel> CreateViewModel()
        {
            List<SelectListItem> aircraft = await aircraftService.GetAircraftForSelect();
            List<SelectListItem> countries = await countryService.GetCountriesForSelect();

            CreateFlightViewModel viewModel = FlightFactory.CreateViewModelForCreation(aircraft, countries);

            return viewModel;
        }

        public async Task<PaginatedList<Flight>> GetAllAsPaginatedListAsync(int pageSize, int? pageNumber)
        {
            pageNumber ??= 1;
            int page = pageNumber.Value;

            Task<List<Flight>> flightTask = GetAllAsync(pageSize, page);
            Task<int> countTask = GetCountAsync();

            await Task.WhenAll(flightTask, countTask);
            PaginatedList<Flight> paginatedItems = new(flightTask.Result, countTask.Result, page, pageSize);

            return paginatedItems;
        }

        public async Task<FlightEditViewModel> GetEditViewModel(string flightId)
        {
            Flight? flight = await GetById(flightId) ??
                throw new ArgumentException($"Flight with id {flightId} does not exist.");

            List<SelectListItem> aircraft = await aircraftService.GetAircraftForSelect();
            List<SelectListItem> countries = await countryService.GetCountriesForSelect();

            IEnumerable<Airport> originAirports = await airportService.GetAllForCountry(flight.Origin.Country.Id);
            IEnumerable<Airport> destinationAirports = await airportService.GetAllForCountry(flight.Destination.Country.Id);

            List<SelectListItem> originAirportsSelect = AirportFactory.CreateAirportForSelect(originAirports);
            List<SelectListItem> destinationAirportsSelect = AirportFactory.CreateAirportForSelect(destinationAirports);

            FlightEditViewModel viewModel = FlightFactory.CreateEditViewModel(flight, aircraft, countries, originAirportsSelect, destinationAirportsSelect);

            return viewModel;
        }
    }
}
