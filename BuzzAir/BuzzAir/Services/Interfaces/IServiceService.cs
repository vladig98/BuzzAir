namespace BuzzAir.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IService> Create(string name, BaggageType baggageType);
        Task<List<IService>> CreateServicesAsync(List<ServiceViewModel> services, BaggageType baggageType);
        List<ServiceViewModel> GetServiceDetails(IEnumerable<Service> services);
        List<ServiceViewModel> GetViewModels();
    }
}
