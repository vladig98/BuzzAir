namespace BuzzAir.Models.ViewModels
{
    public class AllBookingsViewModel
    {
        public AllBookingsViewModel()
        {
            this.Bookings = new List<BookingViewModel>();
        }

        public List<BookingViewModel> Bookings { get; set; }
    }
}
