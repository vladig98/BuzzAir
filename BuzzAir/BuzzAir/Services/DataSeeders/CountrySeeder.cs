using BuzzAir.Services.DataSeeders.Interfaces;

namespace BuzzAir.Services.DataSeeders;

public class CountrySeeder(BuzzAirDbContext dbContext) : IDataSeeder
{
    private const string _parentFolderPath = @"..\..\..\..";
    private const string _dataProjectName = "Buzzair.Data";
    private const string _seedDataFolderName = "Seed";
    private const string _jsonFileName = "countries.json";

    public async Task SeedAsync()
    {
        string rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, _parentFolderPath));
        string seedFolder = Path.Combine(rootPath, _dataProjectName, _seedDataFolderName);

        if (await dbContext.Countries.AnyAsync())
        {
            return;
        }

        string filePath = Path.Combine(seedFolder, _jsonFileName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Seed file not found: {filePath}");
        }

        string jsonContent = await File.ReadAllTextAsync(filePath);
        CountryJsonDto[]? jsonData = JsonConvert.DeserializeObject<CountryJsonDto[]>(jsonContent);

        if (jsonData is null || jsonData.Length == 0)
        {
            return;
        }

        Country[] countries = [.. jsonData.Select(x => new Country()
        {
            ISOA2 = x.ISOA2,
            ISOA3 = x.ISOA3,
            IsOfficiallyRecognizedCountry = x.IsOfficiallyRecognizedCountry,
            Name = x.Name
        })];

        await dbContext.Countries.AddRangeAsync(countries);
        _ = await dbContext.SaveChangesAsync();
    }
}
