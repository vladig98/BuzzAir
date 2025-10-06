namespace BuzzAir.Services.DataSeeders;

public class AircraftSeeder(BuzzAirDbContext dbContext) : IDataSeeder
{
    private const string _parentFolderPath = @"..\..\..\..";
    private const string _dataProjectName = "Buzzair.Data";
    private const string _seedDataFolderName = "Seed";
    private const string _jsonFileName = "aircraft.json";

    public async Task SeedAsync()
    {
        string rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, _parentFolderPath));
        string seedFolder = Path.Combine(rootPath, _dataProjectName, _seedDataFolderName);

        if (await dbContext.Aircrafts.AnyAsync())
        {
            return;
        }

        string filePath = Path.Combine(seedFolder, _jsonFileName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Seed file not found: {filePath}");
        }

        string jsonContent = await File.ReadAllTextAsync(filePath);
        AircraftJsonDto[]? jsonData = JsonConvert.DeserializeObject<AircraftJsonDto[]>(jsonContent);

        if (jsonData is null || jsonData.Length == 0)
        {
            return;
        }

        Aircraft[] aircraft = [.. jsonData.Select(x => new Aircraft()
        {
            Name = x.Name,
            NumberOfSeats = x.Seats
        })];

        await dbContext.Aircrafts.AddRangeAsync(aircraft);
        _ = await dbContext.SaveChangesAsync();
    }
}
