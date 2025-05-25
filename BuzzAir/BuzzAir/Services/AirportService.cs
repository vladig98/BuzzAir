namespace BuzzAir.Services
{
    public class AirportService(
        BuzzAirDbContext context,
        ICountryService countryService,
        ITimezoneService timezoneService,
        ICityService cityService,
        IStateService stateService) : IAirportService
    {
        public async Task CreateAsync(AirportCreateViewModel model)
        {
            Task<Country> countryTask = countryService.GetByIdAsync(model.Country);
            Task<State?> stateTask = stateService.GetByIdAsync(model.State);
            Task<City> cityTask = cityService.GetByIdAsync(model.City);

            await Task.WhenAll(countryTask, stateTask, cityTask);
            Airport airport = AirportFactory.CreateAirport(model, countryTask.Result, cityTask.Result, stateTask.Result);

            await context.Airports.AddAsync(airport);
            await context.SaveChangesAsync();
        }

        public async Task<AirportCreateViewModel> GetCreateViewModelAsync()
        {
            IEnumerable<Country> coutries = await countryService.GetAllAsync();
            IEnumerable<Timezone> timezones = await timezoneService.GetAllAsync();
            IEnumerable<City> cities = await cityService.GetAllAsync();
            IEnumerable<State> states = await stateService.GetAllAsync();

            List<SelectListItem> countrySelect = CountryFactory.GetCountriesForSelect([.. coutries]);
            List<SelectListItem> timezoneSelect = TimezoneFactory.GetTimezonesForSelect([.. timezones]);

            AirportCreateViewModel model = AirportFactory.CreateViewModelForCreation(countrySelect, timezoneSelect, [.. cities], [.. states]);

            return model;
        }

        public async Task<AirportViewModel> GetEditViewModelAsync(string airportId)
        {
            Airport airport = await GetByIdAsync(airportId);
            AirportViewModel model = AirportFactory.GetEditViewModel(airport);

            return model;
        }

        public async Task<int> GetCount()
        {
            return await context.Airports.Where(x => !x.IsDeleted).CountAsync();
        }

        public async Task<IEnumerable<Airport>> GetAll()
        {
            return await context.Airports.Include(x => x.Country).AsSplitQuery().ToListAsync();
        }

        public async Task<IEnumerable<Airport>> GetAllForCountry(string countryId)
        {
            return await context.Airports.Include(x => x.Country).Where(x => x.CountryId == countryId).AsSplitQuery().ToListAsync();
        }

        public async Task<Airport> GetByIdAsync(string id)
        {
            return await context.Airports.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new ArgumentException($"Can't find airport with id {id}");
        }

        public async Task<PaginatedList<Airport>> GetAllAsync(int pageSize, int? pageNumber)
        {
            pageNumber ??= 1;
            int page = pageNumber.Value;

            Task<IEnumerable<Airport>> airportsTask = GetAllAsync(pageSize, page);
            Task<int> countTask = GetCount();

            await Task.WhenAll(airportsTask, countTask);

            PaginatedList<Airport> airports = new(airportsTask.Result, countTask.Result, page, pageSize);

            return airports;
        }

        private async Task<IEnumerable<Airport>> GetAllAsync(int pageSize, int pageNumber)
        {
            int toSkip = (pageNumber - 1) * pageSize;

            return await context.Airports
                .Include(x => x.City)
                .Include(x => x.State)
                .Include(x => x.Country)
                .Where(x => !x.IsDeleted)
                .Skip(toSkip)
                .Take(pageSize)
                .OrderBy(x => x.Name)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
