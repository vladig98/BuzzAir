namespace BuzzAir.Factories;

internal static class CacheFactory
{
    public static ICachingService GetCachingService(
        string? connectionString,
        IMemoryCache memoryCache,
        BuzzAirDbContext dbContext)
    {
        ICachingService cachingService;

        try
        {
            string redisConnectionString = connectionString ??
                throw new InvalidOperationException("Connection string 'Redis' not found.");

            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            IDatabase redis = connectionMultiplexer.GetDatabase();

            cachingService = new RedisCachingService(redis);
        }
        catch (Exception)
        {
            cachingService = new InMemoryCachingService(memoryCache);
        }

        City[] cities = [.. dbContext.Cities.Include(x => x.State).Include(x => x.Country).AsNoTracking()];
        State[] states = [.. dbContext.States.Include(x => x.Country).AsNoTracking()];
        Country[] countries = [.. dbContext.Countries.AsNoTracking()];
        Aircraft[] aircraft = [.. dbContext.Aircrafts.AsNoTracking()];
        Airport[] airports = [.. dbContext.Airports.Include(x => x.City).ThenInclude(x => x.State).Include(x => x.City).ThenInclude(x => x.Country).AsNoTracking()];

        foreach (City city in cities)
        {
            string key = string.Format(CultureInfo.InvariantCulture, GlobalConstants.CITY_CACHE_KEY, city.Id);
            _ = cachingService.SetAsync(key, city, CancellationToken.None);
        }

        foreach (State state in states)
        {
            string key = string.Format(CultureInfo.InvariantCulture, GlobalConstants.STATE_CACHE_KEY, state.Id);
            _ = cachingService.SetAsync(key, state, CancellationToken.None);
        }

        foreach (Country country in countries)
        {
            string key = string.Format(CultureInfo.InvariantCulture, GlobalConstants.COUNTRY_CACHE_KEY, country.Id);
            _ = cachingService.SetAsync(key, country, CancellationToken.None);
        }

        foreach (Aircraft air in aircraft)
        {
            string key = string.Format(CultureInfo.InvariantCulture, GlobalConstants.AIRCRAFT_CACHE_KEY, air.Id);
            _ = cachingService.SetAsync(key, air, CancellationToken.None);
        }

        foreach (Airport airport in airports)
        {
            string key = string.Format(CultureInfo.InvariantCulture, GlobalConstants.AIRPORT_CACHE_KEY, airport.Id);
            _ = cachingService.SetAsync(key, airport, CancellationToken.None);
        }

        _ = cachingService.SetAsync(GlobalConstants.CITIES_CACHE_KEY, cities.Where(x => !x.IsDeleted), CancellationToken.None);
        _ = cachingService.SetAsync(GlobalConstants.COUNTRIES_CACHE_KEY, countries.Where(x => !x.IsDeleted), CancellationToken.None);
        _ = cachingService.SetAsync(GlobalConstants.STATES_CACHE_KEY, states.Where(x => !x.IsDeleted), CancellationToken.None);
        _ = cachingService.SetAsync(GlobalConstants.AIRCRAFT_ALL_CACHE_KEY, aircraft.Where(x => !x.IsDeleted), CancellationToken.None);
        _ = cachingService.SetAsync(GlobalConstants.AIRPORTS_CACHE_KEY, airports.Where(x => !x.IsDeleted), CancellationToken.None);

        return cachingService;
    }
}
