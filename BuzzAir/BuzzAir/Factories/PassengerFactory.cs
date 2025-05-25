using BuzzAir.Models.DbModels;
using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Factories
{
    public static class PassengerFactory
    {
        public static PassengerViewModel CreateViewModel(Passenger passenger, List<ServiceViewModel> serviceViewModels)
        {
            PassengerViewModel viewModel = new PassengerViewModel()
            {
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                Gender = passenger.Gender,
                Services = serviceViewModels
            };

            return viewModel;
        }

        public static Passenger Create(PassengerViewModel model)
        {
            Passenger passenger = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                BaggageType = model.BaggageType
            };

            return passenger;
        }

        public static FlightPassenger CreatePassengerForFlight(Flight flight, IPassenger passenger, FlightSeat seat)
        {
            FlightPassenger flightPax = new()
            {
                Flight = flight,
                FlightId = flight.Id,
                Id = Guid.NewGuid().ToString(),
                Person = (Person)passenger,
                PersonId = ((Person)passenger).Id,
                SeatNumber = seat.SeatNumber
            };

            return flightPax;
        }

        internal static PassengerViewModel CreateViewModel(List<ServiceViewModel> services)
        {
            PassengerViewModel viewModel = new PassengerViewModel()
            {
                Services = services
            };

            return viewModel;
        }
    }
}
