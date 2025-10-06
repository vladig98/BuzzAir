using BuzzAir.Services.DataSeeders.Interfaces;

namespace BuzzAir.Services.DataSeeders;

public class TimezoneSeeder(BuzzAirDbContext dbContext) : IDataSeeder
{
    private const string _parentFolderPath = @"..\..\..\..";
    private const string _dataProjectName = "Buzzair.Data";
    private const string _seedDataFolderName = "Seed";
    private const string _jsonFileName = "timezones.json";

    public async Task SeedAsync()
    {
        string rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, _parentFolderPath));
        string seedFolder = Path.Combine(rootPath, _dataProjectName, _seedDataFolderName);

        if (await dbContext.Timezones.AnyAsync())
        {
            return;
        }

        string filePath = Path.Combine(seedFolder, _jsonFileName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Seed file not found: {filePath}");
        }

        string jsonContent = await File.ReadAllTextAsync(filePath);
        TimezoneJsonDto[]? jsonData = JsonConvert.DeserializeObject<TimezoneJsonDto[]>(jsonContent);

        if (jsonData is null || jsonData.Length == 0)
        {
            return;
        }

        Timezone[] timezones = [.. jsonData.Select(x => new Timezone()
        {
            Name = x.Name,
            Identifier = x.Identifier,
            Abbreviation = x.Abbreviation,
            UsesDST = x.UsesDST,
            Offset = TimeSpan.FromMinutes(x.Offset)
        })];

        await dbContext.Timezones.AddRangeAsync(timezones);
        _ = await dbContext.SaveChangesAsync();
    }
}
