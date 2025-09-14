namespace BuzzAir.Services;

public class ServicesService(BuzzAirDbContext dbContext) : IServicesService
{
    public async Task<List<ServiceDto>> GetBaggageServicesAsync(CancellationToken token)
    {
        List<Baggage> listOfBaggage = await dbContext.Baggages.ToListAsync(token);
        List<ServiceDto> services = [];

        foreach (Baggage baggage in listOfBaggage)
        {
            services.Add(new ServiceDto()
            {
                Id = baggage.Id,
                Name = baggage.Name,
                Price = baggage.Price,
                BaggageType = baggage.BaggageType,
                Kilos = baggage.Kilos
            });
        }

        return services;
    }

    public async Task<List<ServiceDto>> GetSeatServicesAsync(CancellationToken token)
    {
        List<Seat> seats = await dbContext.Seats.ToListAsync(token);
        List<ServiceDto> services = [];

        foreach (Seat seat in seats)
        {
            services.Add(new ServiceDto()
            {
                Id = seat.Id,
                Name = seat.Name,
                Price = seat.Price,
                SeatType = seat.SeatType
            });
        }

        return services;
    }

    public async Task<List<ServiceDto>> GetServicesAsync(CancellationToken token)
    {
        List<AirportCheckIn> airportCheckIns = await dbContext.AirportCheckIns.ToListAsync(token);
        List<Flexibility> flexibilities = await dbContext.Flexibilities.ToListAsync(token);
        List<OnTimeArrival> onTimeArrivals = await dbContext.OnTimeArrivals.ToListAsync(token);
        List<Priority> priorities = await dbContext.Priorities.ToListAsync(token);

        List<ServiceDto> services = [];

        foreach (AirportCheckIn airportCheckIn in airportCheckIns)
        {
            services.Add(new ServiceDto()
            {
                Id = airportCheckIn.Id,
                Name = airportCheckIn.Name,
                Price = airportCheckIn.Price
            });
        }

        foreach (Flexibility flexibility in flexibilities)
        {
            services.Add(new ServiceDto()
            {
                Id = flexibility.Id,
                Name = flexibility.Name,
                Price = flexibility.Price
            });
        }

        foreach (OnTimeArrival onTimeArrival in onTimeArrivals)
        {
            services.Add(new ServiceDto()
            {
                Id = onTimeArrival.Id,
                Name = onTimeArrival.Name,
                Price = onTimeArrival.Price
            });
        }

        foreach (Priority priority in priorities)
        {
            services.Add(new ServiceDto()
            {
                Id = priority.Id,
                Name = priority.Name,
                Price = priority.Price
            });
        }

        return services;
    }
}
