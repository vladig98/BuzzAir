namespace BuzzAir.Services.DataSeeders;

public class FlightsSeeder(BuzzAirDbContext dbContext) : IDataSeeder
{
    public async Task SeedAsync()
    {
        DateTime utcNow = DateTime.UtcNow;

        if (await dbContext.Flights.AnyAsync(x => x.DepartureUTC > utcNow))
        {
            return;
        }

        DateTime startOfToday = new(utcNow.Year, utcNow.Month, utcNow.Day);
        DateTime tomorrow = startOfToday.AddDays(1);
        DateTime monthAhead = tomorrow.AddMonths(1);
        decimal min_price = 9.99M;
        decimal max_price = 999.99M;

        List<decimal> prices = [.. Enumerable.Range(0, (int)((max_price - min_price) / 5)).Select(i => min_price + i * 5)];

        int diff = (int)(monthAhead - tomorrow).TotalSeconds;
        int numberOfFlights = 10_000;

        List<string> aircraft = await dbContext.Aircrafts.Select(x => x.Id).ToListAsync();
        List<string> airports = await dbContext.Airports.Select(x => x.Id).ToListAsync();

        List<Flight> flights = [];

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA5394 // Do not use insecure randomness
        for (int i = 0; i < numberOfFlights; i++)
        {
            int flightDuration = Random.Shared.Next(30 * 60, 6 * 60 * 60);

            Flight flight = new()
            {
                AircraftId = aircraft[Random.Shared.Next(aircraft.Count)],
                OriginId = airports[Random.Shared.Next(airports.Count)],
                DepartureUTC = tomorrow.AddSeconds(Random.Shared.Next(1, diff)),
                FlightNumber = $"BZ-{Random.Shared.Next(1000, 9999)}",
                PriceInEur = prices[Random.Shared.Next(prices.Count)],
            };

            flight.ArrivalUTC = flight.DepartureUTC.AddSeconds(flightDuration);

            do
            {
                flight.DestinationId = airports[Random.Shared.Next(airports.Count)];
            } while (flight.OriginId == flight.DestinationId);

            flight.DepartureUTC = DateTime.SpecifyKind(flight.DepartureUTC, DateTimeKind.Utc);
            flight.ArrivalUTC = DateTime.SpecifyKind(flight.ArrivalUTC, DateTimeKind.Utc);

            flights.Add(flight);
        }
#pragma warning restore CA5394 // Do not use insecure randomness
#pragma warning restore IDE0079 // Remove unnecessary suppression

        await dbContext.Flights.AddRangeAsync(flights);
        _ = await dbContext.SaveChangesAsync();
    }
}
