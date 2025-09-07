namespace BuzzAir.Areas.Admin.Services;

public class FlightService(
    BuzzAirDbContext dbContext,
    IAirportService airportService,
    IAircraftService aircraftService) : IFlightService
{
    public async Task AddFlightAsync(CreateFlightVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        Airport origin = await airportService.GetAirportModelByIdAsync(viewModel.OriginId, token);
        Airport destination = await airportService.GetAirportModelByIdAsync(viewModel.DestinationId, token);
        Aircraft aircraft = await aircraftService.GetAicraftModelByIdAsync(viewModel.AircraftId, token);

        Flight flight = new()
        {
            Aircraft = aircraft,
            AircraftId = aircraft.Id,
            Destination = destination,
            DestinationId = destination.Id,
            Origin = origin,
            OriginId = origin.Id,
            ArrivalUTC = DateTime.SpecifyKind(viewModel.ArrivalUTC, DateTimeKind.Utc),
            DepartureUTC = DateTime.SpecifyKind(viewModel.DepartureUTC, DateTimeKind.Utc),
            FlightNumber = viewModel.FlightNumber,
            PriceInEur = viewModel.PriceInEur
        };

        _ = await dbContext.Flights.AddAsync(flight, token);
        _ = await dbContext.SaveChangesAsync(token);
    }

    public Task DeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Flights.Where(c => c.Id == id && !c.IsDeleted).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => true), token);
    }

    public async Task<List<FlightDTO>> GetAllDeletedFlightsAsync(int pageNumber, int itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Cities.CountAsync(c => c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        pageNumber = Math.Clamp(pageNumber, 1, count);
        itemsPerPage = Math.Clamp(itemsPerPage, 10, 100);

        List<Flight> flights = await dbContext.Flights
            .Include(c => c.Origin)
            .Include(c => c.Destination)
            .Include(c => c.Aircraft)
            .Where(x => x.IsDeleted)
            .OrderBy(c => c.DepartureUTC)
            .Skip((pageNumber - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .AsNoTracking()
            .ToListAsync(token);

        List<FlightDTO> dtos = [.. flights.Select(c => new FlightDTO(
            c.Id,
            c.FlightNumber,
            c.Origin.Name,
            c.OriginId,
            c.Destination.Name,
            c.DestinationId,
            c.Aircraft.Name,
            c.AircraftId,
            c.DepartureUTC,
            c.ArrivalUTC,
            c.PriceInEur,
            c.TakenSeats)
        )];

        return dtos;
    }

    public async Task<List<FlightDTO>> GetAllFlightsAsync(int pageNumber, int itemsPerPage, CancellationToken token = default)
    {
        int count = await dbContext.Cities.CountAsync(c => !c.IsDeleted, token);

        if (count == 0)
        {
            return [];
        }

        pageNumber = Math.Clamp(pageNumber, 1, count);
        itemsPerPage = Math.Clamp(itemsPerPage, 10, 100);

        List<Flight> flights = await dbContext.Flights
            .Include(c => c.Origin)
            .Include(c => c.Destination)
            .Include(c => c.Aircraft)
            .Where(x => !x.IsDeleted)
            .OrderBy(c => c.DepartureUTC)
            .Skip((pageNumber - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .AsNoTracking()
            .ToListAsync(token);

        List<FlightDTO> dtos = [.. flights.Select(c => new FlightDTO(
            c.Id,
            c.FlightNumber,
            c.Origin.Name,
            c.OriginId,
            c.Destination.Name,
            c.DestinationId,
            c.Aircraft.Name,
            c.AircraftId,
            c.DepartureUTC,
            c.ArrivalUTC,
            c.PriceInEur,
            c.TakenSeats)
        )];

        return dtos;
    }

    public Task<int> GetCountAsync(CancellationToken token = default)
    {
        return dbContext.Flights.CountAsync(c => !c.IsDeleted, token);
    }

    public Task<int> GetDeletedCountAsync(CancellationToken token = default)
    {
        return dbContext.Flights.CountAsync(c => c.IsDeleted, token);
    }

    public async Task<FlightDTO> GetDeletedFlightByIdAsync(string id, CancellationToken token = default)
    {
        Flight flight = await dbContext.Flights
                                          .Include(c => c.Origin)
                                          .Include(c => c.Destination)
                                          .Include(c => c.Aircraft)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find a city with id {id}");

        FlightDTO dto = new(
            flight.Id,
            flight.FlightNumber,
            flight.Origin.Name,
            flight.OriginId,
            flight.Destination.Name,
            flight.DestinationId,
            flight.Aircraft.Name,
            flight.AircraftId,
            flight.DepartureUTC,
            flight.ArrivalUTC,
            flight.PriceInEur,
            flight.TakenSeats);

        return dto;
    }

    public async Task<FlightDTO> GetFlightByIdAsync(string id, CancellationToken token = default)
    {
        Flight flight = await dbContext.Flights
                                          .Include(c => c.Origin)
                                          .Include(c => c.Destination)
                                          .Include(c => c.Aircraft)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, token)
            ?? throw new KeyNotFoundException($"Can't find a city with id {id}");

        FlightDTO dto = new(
            flight.Id,
            flight.FlightNumber,
            flight.Origin.Name,
            flight.OriginId,
            flight.Destination.Name,
            flight.DestinationId,
            flight.Aircraft.Name,
            flight.AircraftId,
            flight.DepartureUTC,
            flight.ArrivalUTC,
            flight.PriceInEur,
            flight.TakenSeats);

        return dto;
    }

    public Task<Dictionary<string, DateTime>> GetFlightsDatesBasedOnOriginAndDestination(string originId, string destinationId, CancellationToken token = default)
    {
        return dbContext.Flights.Where(x => x.DepartureUTC > DateTime.UtcNow && !x.IsDeleted && x.OriginId == originId && x.DestinationId == destinationId)
                                .ToDictionaryAsync(x => x.Id, x => x.DepartureUTC, token);
    }

    public Task<Dictionary<string, string>> GetFutureFlightsDestinationsBasedOnOriginAsync(string originId, CancellationToken token = default)
    {
        return dbContext.Flights.Where(x => x.DepartureUTC > DateTime.UtcNow && !x.IsDeleted && x.OriginId == originId)
                                .Include(x => x.Destination)
                                .Select(x => new { x.DestinationId, x.Destination.Name })
                                .OrderBy(x => x.Name)
                                .GroupBy(x => x.DestinationId)
                                .ToDictionaryAsync(x => x.Key, x => x.First().Name, token);
    }

    public Task<Dictionary<string, string>> GetFutureFlightsOriginsAsync(CancellationToken token = default)
    {
        return dbContext.Flights.Where(x => x.DepartureUTC > DateTime.UtcNow && !x.IsDeleted)
                                .Include(x => x.Origin)
                                .Select(x => new { x.OriginId, x.Origin.Name })
                                .OrderBy(x => x.Name)
                                .GroupBy(x => x.OriginId)
                                .ToDictionaryAsync(x => x.Key, x => x.First().Name, token);
    }

    public Task<Dictionary<string, DateTime>> GetReturnFlightsDatesBasedOnOriginAndDestination(string originId, string destinationId, DateTime earliest, CancellationToken token = default)
    {
        return dbContext.Flights.Where(x => x.DepartureUTC > earliest && !x.IsDeleted && x.OriginId == originId && x.DestinationId == destinationId)
                                .ToDictionaryAsync(x => x.Id, x => x.DepartureUTC, token);
    }

    public Task HardDeleteAsync(string id, CancellationToken token = default)
    {
        return dbContext.Flights.Where(c => c.Id == id && !c.IsDeleted).ExecuteDeleteAsync(token);
    }

    public Task RestoreAsync(string id, CancellationToken token = default)
    {
        return dbContext.Flights.Where(c => c.Id == id && c.IsDeleted).ExecuteUpdateAsync(c => c.SetProperty(p => p.IsDeleted, p => false), token);
    }

    public Task UpdateFlightAsync(EditFlightVM viewModel, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return dbContext.Flights.Where(c => c.Id == viewModel.Id && !c.IsDeleted)
            .ExecuteUpdateAsync(p => p
                .SetProperty(
                    c => c.OriginId,
                    c => viewModel.OriginId)
                .SetProperty(
                    c => c.DestinationId,
                    c => viewModel.DestinationId)
                .SetProperty(
                    c => c.AircraftId,
                    c => viewModel.AircraftId)
                .SetProperty(
                    c => c.FlightNumber,
                    c => viewModel.FlightNumber)
                .SetProperty(
                    c => c.ArrivalUTC,
                    c => DateTime.SpecifyKind(viewModel.ArrivalUTC, DateTimeKind.Utc))
                .SetProperty(
                    c => c.DepartureUTC,
                    c => DateTime.SpecifyKind(viewModel.DepartureUTC, DateTimeKind.Utc))
                .SetProperty(
                    c => c.PriceInEur,
                    c => viewModel.PriceInEur), token);
    }
}
