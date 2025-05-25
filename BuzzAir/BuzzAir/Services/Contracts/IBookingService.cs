

namespace BuzzAir.Services.Contracts
{
    public interface IBookingService
    {
        Task<bool> ExistsByIdAsync(string id);
        Task<Booking> GetByIdAsync(string id);
        Task DeleteAsync(string id);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task CreateAsync(Payment payment);
        Task<BookingViewModel> GetBookingDetailsAsync(string id);
        Task<AllBookingsViewModel> GetAllAsync(string? name);
        Task CreateAsync(CreateBookingViewModel model);
        Task<CreateBookingViewModel> CreateViewModelAsync(IndexViewModel model);
    }
}
