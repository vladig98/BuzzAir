namespace BuzzAir.Services;

public class DataSeeder(IServiceProvider serviceProvider) : IDataSeeder
{
    private const string _parentFolderPath = @"..\..\..\..";
    private const string _dataProjectName = "Buzzair.Data";
    private const string _seedDataFolderName = "Seed";

    private const string _countriesJsonName = "countries.json";
    private const string _timezonesJsonName = "timezones.json";
    private const string _citiesJsonName = "cities.json";
    private const string _aircraftJsonName = "aircraft.json";
    private const string _airportsJsonName = "airports.json";

    public async Task SeedAsync()
    {
        string rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, _parentFolderPath));
        string seedFolder = Path.Combine(rootPath, _dataProjectName, _seedDataFolderName);

        using IServiceScope scope = serviceProvider.CreateScope();
        BuzzAirDbContext dbContext = scope.ServiceProvider.GetRequiredService<BuzzAirDbContext>();

        if (dbContext.Database.IsRelational())
        {
            await dbContext.Database.MigrateAsync();
        }

        await SeedAsync<Country, CountryJsonDto>(dbContext.Countries, seedFolder, _countriesJsonName, dto => new Country()
        {
            Name = dto.Name,
            ISOA2 = dto.ISOA2,
            ISOA3 = dto.ISOA3,
            IsOfficiallyRecognizedCountry = dto.IsOfficiallyRecognizedCountry
        });

        await SeedAsync<Timezone, TimezoneJsonDto>(dbContext.Timezones, seedFolder, _timezonesJsonName, dto => new Timezone()
        {
            Name = dto.Name,
            Identifier = dto.Identifier,
            Abbreviation = dto.Abbreviation,
            UsesDST = dto.UsesDST,
            Offset = TimeSpan.FromMinutes(dto.Offset)
        });

        await SeedAsync<Aircraft, AircraftJsonDto>(dbContext.Aircrafts, seedFolder, _aircraftJsonName, dto => new Aircraft()
        {
            Name = dto.Name,
            NumberOfSeats = dto.Seats
        });

        _ = await dbContext.SaveChangesAsync();

        Dictionary<string, Country> existingCountriesByName = await dbContext.Countries.AsNoTracking().ToDictionaryAsync(x => x.Name, x => x);
        Dictionary<string, Country> existingCountriesByISOA2 = await dbContext.Countries.AsNoTracking().ToDictionaryAsync(x => x.ISOA2, x => x);
        Dictionary<string, State> existingStatesByName = await dbContext.States
            .Include(x => x.Country)
            .AsNoTracking()
            .ToDictionaryAsync(x => $"{x.Name}__{x.Country.Name}", x => x);

        Dictionary<string, Timezone> existingTimezones = await dbContext.Timezones.AsNoTracking().ToDictionaryAsync(x => x.Name, x => x);

        List<State> newStates = [];
        List<City> newCities = [];

        Timezone utc = existingTimezones["Etc/UTC"];
        await SeedAsync<City, CityJsonDto>(dbContext.Cities, seedFolder, _citiesJsonName, dto => new City()
        {
            Name = dto.City,
            CountryId = GetCountryId(dto.Country, existingCountriesByName),
            StateId = GetStateId(dto.State, dto.Country, existingStatesByName, existingCountriesByName, newStates),
            TimezoneId = utc.Id
        });

        await dbContext.States.AddRangeAsync(newStates);
        _ = await dbContext.SaveChangesAsync();

        newStates = [];
        Dictionary<string, City> existingCitiesByName = await dbContext.Cities
            .Include(x => x.State)
            .Include(x => x.Country)
            .ToDictionaryAsync(x => $"{x.Name}__{x.State?.Name}__{x.Country.ISOA2}", x => x);

        await SeedAsync<Airport, AirportJsonData>(dbContext.Airports, seedFolder, _airportsJsonName, dto => new Airport()
        {
            Name = dto.Name,
            IATA = dto.Iata,
            ICAO = dto.Icao,
            ElevationAboveSeaLevel = dto.Elevation,
            Latitude = dto.Lat,
            Longitude = dto.Lon,
            CityId = GetCityId(dto.City, dto.State, dto.Country, dto.Tz, newCities, newStates, existingCitiesByName, existingStatesByName, existingCountriesByName, existingCountriesByISOA2, existingTimezones)
        });

        await dbContext.States.AddRangeAsync(newStates);
        await dbContext.Cities.AddRangeAsync(newCities);
        _ = await dbContext.SaveChangesAsync();
    }

    private static string GetCityId(
        string cityName,
        string stateName,
        string countryIsoA2,
        string tz,
        List<City> newCities,
        List<State> newStates,
        Dictionary<string, City> existingCitiesByName,
        Dictionary<string, State> existingStatesByName,
        Dictionary<string, Country> existingCountriesByName,
        Dictionary<string, Country> existingCountriesByISOA2,
        Dictionary<string, Timezone> existingTimezones)
    {
        string key = CityKey(cityName, stateName, countryIsoA2);
        City? city = existingCitiesByName.TryGetValue(key, out City? dbEntity) ? dbEntity : null;

        Timezone timezone = existingTimezones[tz];
        if (city is null)
        {
            Country country = existingCountriesByISOA2[countryIsoA2];
            string? stateId = GetStateId(stateName, country.Name, existingStatesByName, existingCountriesByName, newStates);

            if (stateId is null && !string.IsNullOrWhiteSpace(stateName))
            {
                State state = new()
                {
                    CountryId = country.Id,
                    Name = stateName
                };

                newStates.Add(state);
                string stateKey = StateKey(stateName, country.Name);

                existingStatesByName.Add(stateKey, state);
                stateId = state.Id;
            }

            city = new City()
            {
                Name = cityName,
                CountryId = country.Id,
                StateId = stateId,
                TimezoneId = timezone.Id,
            };

            newCities.Add(city);
            existingCitiesByName.Add(key, city);
        }
        else
        {
            city.TimezoneId = timezone.Id;
        }

        return city.Id;
    }

    private static string? GetStateId(
        string stateName,
        string countryName,
        Dictionary<string, State> existingStatesByName,
        Dictionary<string, Country> existingCountriesByName,
        List<State> newStates)
    {
        if (string.IsNullOrWhiteSpace(stateName))
        {
            return null;
        }

        string key = StateKey(stateName, countryName);
        State? state = existingStatesByName.TryGetValue(key, out State? dbEntity) ? dbEntity : null;

        if (state is null)
        {
            state = new State()
            {
                Name = stateName,
                CountryId = GetCountryId(countryName, existingCountriesByName)
            };

            existingStatesByName.Add(key, state);
            newStates.Add(state);
        }

        return state.Id;
    }

    private static string GetCountryId(string countryName, Dictionary<string, Country> existingCountriesByName)
    {
        // It's okay to throw, we don't expect missing countries
        Country country = existingCountriesByName[countryName];

        return country.Id;
    }

    private static string CityKey(string cityName, string stateName, string countryName)
    {
        return $"{cityName}__{stateName}__{countryName}";
    }

    private static string StateKey(string stateName, string countryName)
    {
        return $"{stateName}__{countryName}";
    }

    private static async Task SeedAsync<TDbModel, TJsonDtoModel>(
        DbSet<TDbModel> dbSet,
        string seedFolder,
        string jsonFileName,
        Func<TJsonDtoModel, TDbModel> mapFunc)
        where TDbModel : class
    {
        if (await dbSet.AnyAsync())
        {
            return;
        }

        string filePath = Path.Combine(seedFolder, jsonFileName);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Seed file not found: {filePath}");
        }

        string jsonContent = await File.ReadAllTextAsync(filePath);
        TJsonDtoModel[]? jsonData = JsonConvert.DeserializeObject<TJsonDtoModel[]>(jsonContent);

        if (jsonData is null || jsonData.Length == 0)
        {
            return;
        }

        List<TDbModel> entities = [.. jsonData.Select(mapFunc)];

        await dbSet.AddRangeAsync(entities);
    }
}
