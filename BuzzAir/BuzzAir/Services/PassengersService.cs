namespace BuzzAir.Services;

public class PassengersService(
    BuzzAirDbContext dbContext,
    IServicesService servicesService,
    ITravelDocumentService travelDocumentService) : IPassengersService
{
    public async Task<Passenger> CreatePassengerAsync(PassengerDto data, CancellationToken token)
    {
        if (data is null)
        {
            throw new InvalidOperationException("Invalid passenger details");
        }

        bool validGender = Enum.TryParse(data.Gender, ignoreCase: true, out Gender gender);

        if (!validGender)
        {
            throw new InvalidOperationException("Invalid passenger details");
        }

        Passenger passenger = new()
        {
            DateOfBirth = DateTime.SpecifyKind(data.DateOfBirth, DateTimeKind.Utc),
            FirstName = data.FirstName,
            Gender = gender,
            LastName = data.LastName
        };

        await AddServices(data.ServiceIds, passenger, token);
        await AddServices(data.Baggage, passenger, token);
        await AddServices(data.Seats, passenger, token);

        TravelDocument? travelDocument = await travelDocumentService.CreateAsync(data.TravelDocument, gender, token);

        if (travelDocument is not null)
        {
            passenger.Document = travelDocument;
        }

        _ = await dbContext.Passengers.AddAsync(passenger, token);

        return passenger;
    }

    private async Task AddServices(IList<string> serviceIds, Passenger passenger, CancellationToken token)
    {
        foreach (string id in serviceIds)
        {
            Service? service = await servicesService.GetServiceModelByIdAsync(id, token);

            if (service is null)
            {
                continue;
            }

            passenger.Services.Add(new PassengerService()
            {
                Passenger = passenger,
                Service = service
            });
        }
    }
}
