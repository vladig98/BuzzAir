using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Services
{
    public class PassengerService(
        BuzzAirDbContext context, 
        IPassengerServiceService passengerServiceService, 
        IServiceService serviceService) : IPassengerService
    {
        public async Task<IPassenger> Create(PassengerViewModel model, ICollection<IService> services)
        {
            Passenger passenger = PassengerFactory.Create(model);

            await context.Passengers.AddAsync(passenger);
            await context.SaveChangesAsync();

            return passenger;
        }

        public async Task<List<IPassenger>> CreatePassengersAsync(List<PassengerViewModel> passengerModels)
        {
            List<IPassenger> passengers = [];

            foreach (PassengerViewModel passengerModel in passengerModels)
            {
                List<IService> services = await serviceService.CreateServicesAsync(passengerModel.Services, passengerModel.BaggageType);
                IPassenger passenger = await Create(passengerModel, services);
                await passengerServiceService.CreateAsync(passenger, services);

                passengers.Add(passenger);
            }

            return passengers;
        }

        public List<PassengerViewModel> GetPassengersDetails(ICollection<BookingPassenger> passengers)
        {
            int count = passengers.Count;
            List<PassengerViewModel> viewModels = new(count);

            foreach (BookingPassenger flightPassenger in passengers)
            {
                Passenger passenger = flightPassenger.Passenger;
                IEnumerable<Service> services = passenger.Services.Select(x => x.Service);
                List<ServiceViewModel> serviceViewModels = serviceService.GetServiceDetails(services);
                PassengerViewModel viewModel = PassengerFactory.CreateViewModel(passenger, serviceViewModels);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public List<PassengerViewModel> GetViewModels(int passengers)
        {
            List<PassengerViewModel> viewModels = new(passengers);

            for (int i = 0; i < passengers; i++)
            {
                List<ServiceViewModel> services = serviceService.GetViewModels();
                PassengerViewModel viewModel = PassengerFactory.CreateViewModel(services);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
