using System;

namespace BuzzAir.Models
{
    public class Flight
    {
        public int Id { get; set; }

        public virtual Airport Origin { get; set; }

        public virtual Airport Destination { get; set; }

        public virtual Aircraft Aircraft { get; set; }

        public int DurationInMinutes { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arriving { get; set; }

        public decimal Price { get; set; }
    }
}