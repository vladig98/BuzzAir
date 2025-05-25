namespace BuzzAir.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> GetByIdAsync(string id);
        Task DeleteAsync(string id);
        Task<BookingViewModel> GetBookingDetailsAsync(string id);
        Task<AllBookingsViewModel> GetAllAsync(string? name);
        Task CreateAsync(CreateBookingViewModel model, string username);
        Task<CreateBookingViewModel> CreateViewModelAsync(IndexViewModel model);
    }
}
