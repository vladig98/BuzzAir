namespace BuzzAir.Services.Interfaces
{
    public interface IPassengerServiceService
    {
        Task<PersonService> Create(IPassenger passenger, IService service);
        Task CreateAsync(IPassenger passenger, List<IService> services);
    }
}
