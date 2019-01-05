using System.Collections.Generic;

namespace BuzzAir.Models
{
    public class BookingViewModel
    {
        public BookingViewModel()
        {
            this.Terminals = new List<string>();
        }

        public string Outbound { get; set; }

        public string Inbound { get; set; }

        public string Amount { get; set; }

        public string User { get; set; }

        public int Id { get; set; }

        public string Currency { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        public List<string> Terminals { get; set; }
    }
}
