namespace BuzzAir.Models.ViewModels
{
    public record AllBookingsViewModel
    {
        public List<BookingViewModel> Bookings { get; set; } = [];
    }
}
