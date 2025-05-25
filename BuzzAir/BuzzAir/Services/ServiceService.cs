namespace BuzzAir.Services
{
    public class ServiceService(BuzzAirDbContext context) : IServiceService
    {
        public async Task<IService> Create(string name, BaggageType baggageType)
        {
            Service service = name switch
            {
                "AirportCheckIn" => ServiceFactory.CreateAirportCheckIn(),
                "Baggage" => ServiceFactory.CreateBaggage(baggageType),
                "Flexibility" => ServiceFactory.CreateFlexibility(),
                "OnTimeArrival" => ServiceFactory.CreateOnTimeArrival(),
                "Priority" => ServiceFactory.CreatePriority(),
                "Seat" => ServiceFactory.CreateSeat(),
                _ => throw new ArgumentException($"Invalid service provided {name}"),
            };

            await context.Services.AddAsync(service);
            await context.SaveChangesAsync();

            return service;
        }

        public async Task<List<IService>> CreateServicesAsync(List<ServiceViewModel> serviceModels, BaggageType baggageType)
        {
            List<Task<IService>> serviceTasks = [];

            foreach (ServiceViewModel serviceModel in serviceModels)
            {
                if (!serviceModel.IsChecked)
                {
                    continue;
                }

                string serviceName = serviceModel.Name;
                Task<IService> serviceTask = Create(serviceName, baggageType);

                serviceTasks.Add(serviceTask);
            }

            await Task.WhenAll(serviceTasks);
            List<IService> services = [];

            foreach (Task<IService> completedTask in serviceTasks)
            {
                services.Add(completedTask.Result);
            }

            return services;
        }

        public List<ServiceViewModel> GetServiceDetails(IEnumerable<Service> services)
        {
            int count = services.Count();
            List<ServiceViewModel> viewModels = new(count);

            foreach (Service service in services)
            {
                ServiceViewModel viewModel = ServiceFactory.CreateViewModel(service);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public List<ServiceViewModel> GetViewModels()
        {
            List<ServiceViewModel> services =
            [
                ServiceFactory.CreateAirportCheckInViewModel(),
                ServiceFactory.CreateBaggageViewModel(),
                ServiceFactory.CreateFlexibilityViewModel(),
                ServiceFactory.CreateOnTimeArrivalViewModel(),
                ServiceFactory.CreatePriorityViewModel(),
                ServiceFactory.CreateSeatViewModel()
            ];

            return services;
        }
    }
}
