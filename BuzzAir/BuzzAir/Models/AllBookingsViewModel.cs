using System.Collections.Generic;

namespace BuzzAir.Models
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
