namespace BuzzAir.Services
{
    public class CityService(
        BuzzAirDbContext context,
        ICountryService countryService,
        IStateService stateService) : ICityService
    {
        public async Task<City> CreateAsync(string name, string countryId, string stateId)
        {
            Task<Country> countryTask = countryService.GetByIdAsync(countryId);
            Task<State?> stateTask = stateService.GetByIdAsync(stateId);

            await Task.WhenAll(countryTask, stateTask);

            City city = CityFactory.Create(name, countryTask.Result);
            State? state = stateTask.Result;

            if (state != null)
            {
                city.State = state;
            }

            await context.Cities.AddAsync(city);
            await context.SaveChangesAsync();

            return city;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await context.Cities.Include(x => x.State).AsSplitQuery().AsNoTracking().ToListAsync();
        }

        public async Task<City> GetByIdAsync(string id)
        {
            return await context.Cities.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new ArgumentException($"Can't find a city with id {id}.");
        }

        public async Task<City> GetByNameAsync(string name)
        {
            return await context.Cities.FirstOrDefaultAsync(x => x.Name == name) ??
                throw new ArgumentException($"Can't find a city with the name {name}.");
        }
    }
}
