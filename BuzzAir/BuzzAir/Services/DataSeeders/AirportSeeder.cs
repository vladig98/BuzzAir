using BuzzAir.Services.DataSeeders.Interfaces;

namespace BuzzAir.Services.DataSeeders;

public class AirportSeeder(BuzzAirDbContext dbContext) : IDataSeeder
{
    private const string _parentFolderPath = @"..\..\..\..";
    private const string _dataProjectName = "Buzzair.Data";
    private const string _seedDataFolderName = "Seed";
    private const string _jsonFileName = "airports.json";

    public async Task SeedAsync()
    {
        string rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, _parentFolderPath));
        string seedFolder = Path.Combine(rootPath, _dataProjectName, _seedDataFolderName);

        if (await dbContext.Airports.AnyAsync())
        {
            return;
        }

        string filePath = Path.Combine(seedFolder, _jsonFileName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Seed file not found: {filePath}");
        }

        string jsonContent = await File.ReadAllTextAsync(filePath);
        AirportJsonData[]? jsonData = JsonConvert.DeserializeObject<AirportJsonData[]>(jsonContent);

        if (jsonData is null || jsonData.Length == 0)
        {
            return;
        }

        Dictionary<string, Country> countries = await dbContext.Countries.AsNoTracking().ToDictionaryAsync(x => x.ISOA2, x => x);
        Dictionary<string, Timezone> timezones = await dbContext.Timezones.AsNoTracking().ToDictionaryAsync(x => x.Name, x => x);
        Dictionary<string, State> states = await dbContext.States
            .Include(x => x.Country)
            .AsNoTracking()
            .ToDictionaryAsync(x => $"{x.Name}__{x.Country.Name}", x => x);
        Dictionary<string, City> cities = await dbContext.Cities
            .Include(x => x.State)
            .Include(x => x.Country)
            .ToDictionaryAsync(x => $"{x.Name}__{x.State?.Name}__{x.Country.ISOA2}", x => x);

        List<State> newStates = [];
        List<City> newCities = [];

        Airport[] airports = [.. jsonData.Select(x => new Airport()
        {
            Name = x.Name,
            IATA = x.Iata,
            ICAO = x.Icao,
            ElevationAboveSeaLevel = x.Elevation,
            Latitude = x.Lat,
            Longitude = x.Lon,
            CityId = GetCityId(x.City, x.State, x.Country, x.Tz, cities, states, countries, timezones, newCities, newStates)
        })];

        await dbContext.Cities.AddRangeAsync(newCities);
        await dbContext.States.AddRangeAsync(newStates);
        await dbContext.Airports.AddRangeAsync(airports);

        _ = await dbContext.SaveChangesAsync();
    }

    private static string GetCityId(
        string cityName,
        string stateName,
        string countryISOA2,
        string timezoneName,
        Dictionary<string, City> cities,
        Dictionary<string, State> states,
        Dictionary<string, Country> countries,
        Dictionary<string, Timezone> timezones,
        List<City> newCities,
        List<State> newStates)
    {
        string key = $"{cityName}__{stateName}__{countryISOA2}";
        Timezone timezone = timezones[timezoneName];

        if (cities.TryGetValue(key, out City? city))
        {
            city.TimezoneId = timezone.Id;
            return city!.Id;
        }

        Country country = countries[countryISOA2];
        State? state = null;

        if (!string.IsNullOrWhiteSpace(stateName))
        {
            string stateKey = $"{stateName}__{country.Name}";
            _ = states.TryGetValue(stateKey, out state);

            if (state is null)
            {
                state = new State()
                {
                    Name = stateName,
                    CountryId = country.Id
                };

                newStates.Add(state);
                states.Add(stateKey, state);
            }
        }

        city = new()
        {
            CountryId = country.Id,
            Name = cityName,
            StateId = state?.Id,
            TimezoneId = timezone.Id
        };

        newCities.Add(city);
        cities.Add(key, city);

        return city.Id;
    }
}
