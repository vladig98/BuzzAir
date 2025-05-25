namespace BuzzAir.Services
{
    public class BoardingPassService(IBookingService bookingService) : IBoardingPassService
    {
        public async Task<BoardingPassViewModel> GetBoardingPassAsync(string bookingId)
        {
            Booking booking = await bookingService.GetByIdAsync(bookingId);
            Flight outbound = booking.Flights.First(x => x.IsOutbound).Flight;
            Flight? inbound = booking.Flights.FirstOrDefault(x => !x.IsOutbound)?.Flight;

            string departure = GetFlightInfo(outbound);
            string arrival = GetFlightInfo(inbound);

            BoardingPassViewModel viewModel = BoardingPassFactory.CreateViewModel(departure, arrival);

            return viewModel;
        }

        private static string GetFlightInfo(Flight? flight)
        {
            if (flight == null)
            {
                return GlobalConstants.OneWayTicket;
            }

            string departure = flight.Departure.ToString(GlobalConstants.TimeFormat);
            string origin = flight.Origin.Name;
            string destination = flight.Destination.Name;
            string arrival = flight.Arrival.ToString(GlobalConstants.TimeFormat);
            string originIATA = flight.Origin.IATA;
            string destinationIATA = flight.Destination.IATA;

            string flightInfo = string.Format(GlobalConstants.BoardingPassFormat, departure, origin, destination, arrival, originIATA, destinationIATA);

            return flightInfo;
        }
    }
}
