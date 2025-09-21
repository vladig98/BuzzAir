namespace BuzzAir.Services;

public class BookingService(
    BuzzAirDbContext dbContext,
    IFlightService flightService,
    IPassengersService passengersService,
    IPaymentService paymentService,
    ISeatService seatsService) : IBookingService
{
    public async Task CreateBookingAsync(CreateBookingDto data, CancellationToken token)
    {
        if (data is null)
        {
            return;
        }

        decimal totalAmountInEuro = 0M;
        Booking booking = new();

        Flight? outbound = await flightService.GetFlightModelByIdAsync(data.OutboundId, token);
        Flight? inbound = await flightService.GetFlightModelByIdAsync(data.InboundId, token);

        totalAmountInEuro += outbound?.PriceInEur ?? 0;
        totalAmountInEuro += inbound?.PriceInEur ?? 0;

        AddBookingFlight(booking, outbound);
        AddBookingFlight(booking, inbound);

        foreach (PassengerDto passengerData in data.Passengers)
        {
            Passenger passenger = await passengersService.CreatePassengerAsync(passengerData, token);
            totalAmountInEuro += passenger.Services.Sum(x => x.Service.Price);

            booking.Passengers.Add(new BookingPassenger()
            {
                Passenger = passenger,
                Booking = booking
            });

            AddFlightPassenger(outbound, passenger, passengerData.SeatSelectionOutbound);
            AddFlightPassenger(inbound, passenger, passengerData.SeatSelectionInbound);
        }

        Payment payment = await paymentService.AddPaymentAsync(data.Payment, totalAmountInEuro, token);
        booking.Payment = payment;

        _ = await dbContext.Bookings.AddAsync(booking, token);
        _ = await dbContext.SaveChangesAsync(token);
    }

    private static void AddBookingFlight(Booking booking, Flight? flight)
    {
        if (flight is null)
        {
            return;
        }

        booking.Flights.Add(new BookingFlight()
        {
            Booking = booking,
            Flight = flight
        });
    }

    private void AddFlightPassenger(Flight? flight, Passenger passenger, int? seatSelection)
    {
        if (flight is null)
        {
            return;
        }

        Seat? seatService = passenger.Services.Select(x => x.Service).OfType<Seat>().FirstOrDefault();

        passenger.Flights.Add(new FlightPassenger()
        {
            Flight = flight,
            Passenger = passenger,
            SeatNumber = seatsService.GetSeatNumberAsync(seatService, flight, passenger, seatSelection)
        });
    }
}
