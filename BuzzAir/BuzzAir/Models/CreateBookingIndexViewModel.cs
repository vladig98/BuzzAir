using System.Collections.Generic;

namespace BuzzAir.Models
{
    public class CreateBookingIndexViewModel
    {
        public CreateBookingIndexViewModel()
        {
            this.FlightsLists = new List<string>();
            this.Flights = new List<Flight>();
        }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        public string Passenger { get; set; }

        public List<string> FlightsLists { get; set; }

        public List<Flight> Flights { get; set; }

        public string Baggage32kgPrice { get; set; }

        public string Baggage20kgPrice { get; set; }

        public string PriorityPrice { get; set; }

        public string NormalSeatPrice { get; set; }

        public string ExtraLegRoomSeatPrice { get; set; }

        public string OnTimeArrivalPrice { get; set; }

        public string AirportCheckInPrice { get; set; }

        public string FlexibilityPrice { get; set; }
    }
}
