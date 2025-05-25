namespace BuzzAir.Services.Interfaces
{
    public interface IPassengerService
    {
        Task<IPassenger> Create(PassengerViewModel viewModel, ICollection<IService> services);
        Task<List<IPassenger>> CreatePassengersAsync(List<PassengerViewModel> passengers);
        List<PassengerViewModel> GetPassengersDetails(ICollection<BookingPassenger> passengers);
        List<PassengerViewModel> GetViewModels(int passengers);
    }
}
