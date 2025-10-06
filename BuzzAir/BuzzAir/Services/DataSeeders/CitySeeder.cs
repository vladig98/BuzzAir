using BuzzAir.Services.DataSeeders.Interfaces;

namespace BuzzAir.Services.DataSeeders;

public class CitySeeder(BuzzAirDbContext dbContext) : IDataSeeder
{
    private const string _parentFolderPath = @"..\..\..\..";
    private const string _dataProjectName = "Buzzair.Data";
    private const string _seedDataFolderName = "Seed";
    private const string _jsonFileName = "cities.json";

    public async Task SeedAsync()
    {
        string rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, _parentFolderPath));
        string seedFolder = Path.Combine(rootPath, _dataProjectName, _seedDataFolderName);

        if (await dbContext.Cities.AnyAsync())
        {
            return;
        }

        string filePath = Path.Combine(seedFolder, _jsonFileName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Seed file not found: {filePath}");
        }

        string jsonContent = await File.ReadAllTextAsync(filePath);
        CityJsonDto[]? jsonData = JsonConvert.DeserializeObject<CityJsonDto[]>(jsonContent);

        if (jsonData is null || jsonData.Length == 0)
        {
            return;
        }

        Dictionary<string, Country> countries = await dbContext.Countries.AsNoTracking().ToDictionaryAsync(x => x.Name, x => x);
        Dictionary<string, State> states = await dbContext.States
            .Include(x => x.Country)
            .AsNoTracking()
            .ToDictionaryAsync(x => $"{x.Name}__{x.Country.Name}", x => x);
        string timezoneId = await dbContext.Timezones.AsNoTracking().Where(x => x.Name == "Etc/UTC").Select(x => x.Id).FirstAsync();

        List<State> newStates = [];

        City[] cities = [.. jsonData.Select(x => new City()
        {
            Name = x.City,
            CountryId = countries[x.Country].Id,
            StateId = GetStateId(x.State, x.Country, states, countries, newStates),
            TimezoneId = timezoneId
        })];

        await dbContext.States.AddRangeAsync(newStates);
        await dbContext.Cities.AddRangeAsync(cities);

        _ = await dbContext.SaveChangesAsync();
    }

    private static string? GetStateId(
        string stateName,
        string countryName,
        Dictionary<string, State> states,
        Dictionary<string, Country> countries,
        List<State> newStates)
    {
        if (string.IsNullOrWhiteSpace(stateName))
        {
            return null;
        }

        string key = $"{stateName}__{countryName}";
        bool stateExists = states.TryGetValue(key, out State? state);

        if (stateExists)
        {
            return state!.Id;
        }

        state = new State()
        {
            Name= stateName,
            CountryId = countries[countryName].Id
        };

        states.Add(key, state);
        newStates.Add(state);

        return state.Id;
    }
}
