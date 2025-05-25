using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Services.Contracts
{
    public interface IPassengerService
    {
        Task<IPassenger> Create(PassengerViewModel viewModel, ICollection<IService> services);
        Task<List<IPassenger>> CreatePassengersAsync(List<PassengerViewModel> passengers);
        List<PassengerViewModel> GetPassengersDetails(ICollection<BookingPassenger> passengers);
        List<PassengerViewModel> GetViewModels(int passengers);
    }
}
