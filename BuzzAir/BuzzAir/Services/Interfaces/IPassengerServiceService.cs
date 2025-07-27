namespace BuzzAir.Services.Interfaces
{
    public interface IPassengerServiceService
    {
        Task<Models.DbModels.PassengerService> Create(IPassenger passenger, IService service);
        Task CreateAsync(IPassenger passenger, List<IService> services);
    }
}
