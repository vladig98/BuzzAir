namespace BuzzAir.Services.DataSeeders;

public class ServicesSeeder(BuzzAirDbContext dbContext) : IDataSeeder
{
    public async Task SeedAsync()
    {
        if (!await dbContext.OnTimeArrivals.AnyAsync())
        {
            _ = await dbContext.OnTimeArrivals.AddAsync(new OnTimeArrival());
        }

        if (!await dbContext.Priorities.AnyAsync())
        {
            _ = await dbContext.Priorities.AddAsync(new Priority());
        }

        if (!await dbContext.AirportCheckIns.AnyAsync())
        {
            _ = await dbContext.AirportCheckIns.AddAsync(new AirportCheckIn());
        }

        if (!await dbContext.Flexibilities.AnyAsync())
        {
            _ = await dbContext.Flexibilities.AddAsync(new Flexibility());
        }

        if (!await dbContext.Seats.AnyAsync())
        {
            _ = await dbContext.Seats.AddAsync(new Seat(SeatType.None));
            _ = await dbContext.Seats.AddAsync(new Seat(SeatType.Normal));
            _ = await dbContext.Seats.AddAsync(new Seat(SeatType.ExtraLegRoom));
        }

        if (!await dbContext.Baggages.AnyAsync())
        {
            _ = await dbContext.Baggages.AddAsync(new Baggage(BaggageType.Cabin));
            _ = await dbContext.Baggages.AddAsync(new Baggage(BaggageType.TwentyKilos));
            _ = await dbContext.Baggages.AddAsync(new Baggage(BaggageType.ThirtyTwoKilos));
        }

        _ = await dbContext.SaveChangesAsync();
    }
}
