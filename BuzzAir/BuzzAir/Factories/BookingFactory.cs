namespace BuzzAir.Factories
{
    public static class BookingFactory
    {
        public static Booking CreateBooking(Payment payment)
        {
            Booking booking = new()
            {
                Payment = payment,
                PaymentId = payment.Id,
                TotalPrice = payment.Amount
            };

            return booking;
        }

        internal static BookingViewModel CreateViewModel(Booking booking, List<FlightViewModel> flights, List<PassengerViewModel> passengers)
        {
            BookingViewModel viewModel = new()
            {
                Amount = booking.TotalPrice.ToString("F2"),
                Inbound = flights.First(x => !x.IsOutbound),
                Outbound = flights.First(x => x.IsOutbound),
                Id = booking.Id,
                Passengers = passengers,
                Currency = booking.Payment.Currency.ToString()
            };

            return viewModel;
        }

        internal static AllBookingsViewModel CreateViewModelForAll(List<BookingViewModel> viewModels)
        {
            AllBookingsViewModel viewModel = new()
            {
                Bookings = viewModels
            };

            return viewModel;
        }

        public static BookingPassenger CreatePassengerForBooking(Booking booking, Person person)
        {
            BookingPassenger bookingPassenger = new()
            {
                BookingId = booking.Id,
                Booking = booking,
                PassengerId = person.Id,
                Passenger = (Passenger)person
            };

            return bookingPassenger;
        }

        public static UserBooking CreateBookingForAUser(Booking booking, ApplicationUser user)
        {
            UserBooking userBooking = new()
            {
                ApplicationUserId = user.Id,
                ApplicationUser = user,
                BookingId = booking.Id,
                Booking = booking
            };

            return userBooking;
        }

        internal static CreateBookingViewModel CreateViewModel(List<FlightViewModel> outboundFlights, List<FlightViewModel> inboundFlights, List<PassengerViewModel> passengers)
        {
            CreateBookingViewModel viewModel = new()
            {
                OutboundFlights = outboundFlights,
                InboundFlights = inboundFlights,
                Passengers = passengers,
                PassengersCount = passengers.Count
            };

            return viewModel;
        }
    }
}
