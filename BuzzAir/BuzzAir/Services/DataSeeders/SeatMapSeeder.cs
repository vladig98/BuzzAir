namespace BuzzAir.Services.DataSeeders;

public class SeatMapSeeder(BuzzAirDbContext dbContext) : IDataSeeder
{
    public async Task SeedAsync()
    {
        if (await dbContext.SeatMaps.AnyAsync())
        {
            return;
        }

        Aircraft[] aircraft = await dbContext.Aircrafts.AsNoTracking().ToArrayAsync();

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA5394 // Do not use insecure randomness
        List<SeatMap> seatMap = [];
        foreach (Aircraft craft in aircraft)
        {
            for (int i = 1; i <= craft.NumberOfSeats; i++)
            {
                seatMap.Add(new SeatMap()
                {
                    AircraftId = craft.Id,
                    SeatNumber = i,
                    SeatType = Random.Shared.NextDouble() < 0.15 ? SeatMapType.ExtraLegRoom : SeatMapType.Normal
                });
            }
        }
#pragma warning restore CA5394 // Do not use insecure randomness
#pragma warning restore IDE0079 // Remove unnecessary suppression

        await dbContext.SeatMaps.AddRangeAsync(seatMap);
        _ = await dbContext.SaveChangesAsync();
    }
}
