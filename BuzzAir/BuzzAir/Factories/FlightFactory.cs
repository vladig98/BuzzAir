namespace BuzzAir.Factories
{
    public static class FlightFactory
    {
        public static FlightViewModel CreateViewModel(BookingFlight flight)
        {
            FlightViewModel viewModel = CreateViewModel(flight.Flight);
            viewModel.IsOutbound = flight.IsOutbound;

            return viewModel;
        }

        public static FlightViewModel CreateViewModel(Flight flight)
        {
            AirportViewModel destination = AirportFactory.GetEditViewModel(flight.Destination);
            AirportViewModel origin = AirportFactory.GetEditViewModel(flight.Origin);

            FlightViewModel viewModel = new()
            {
                Id = flight.Id,
                Arrival = flight.Arrival,
                Departure = flight.Departure,
                Destination = destination,
                FlightNumber = flight.FlightNumber,
                Origin = origin,
                Price = flight.Price
            };

            return viewModel;
        }

        internal static BookingFlight CreateFlightForBooking(Booking booking, Flight flight, bool isOutbound)
        {
            BookingFlight bookingFlight = new()
            {
                Flight = flight,
                FlightId = flight.Id,
                Booking = booking,
                BookingId = booking.Id,
                IsOutbound = isOutbound
            };

            return bookingFlight;
        }

        internal static List<SelectListItem> GetFlightsForSelect(List<Flight> flights)
        {
            List<SelectListItem> list = [];
            Dictionary<string, SelectListGroup> groups = [];

            foreach (Flight flight in flights)
            {
                string countryName = flight.Origin.Country.Name;

                if (!groups.TryGetValue(countryName, out SelectListGroup? group))
                {
                    group = new SelectListGroup()
                    {
                        Name = countryName
                    };

                    groups.Add(countryName, group);
                }

                SelectListItem flightItem = new()
                {
                    Text = flight.Origin.City.Name,
                    Value = flight.Origin.City.Id,
                    Group = group
                };

                list.Add(flightItem);
            }

            return list;
        }
    }
}
