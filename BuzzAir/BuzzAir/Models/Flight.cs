﻿using System;
using System.Collections.Generic;

namespace BuzzAir.Models
{
    public class Flight
    {
        public Flight()
        {
            this.Passengers = new HashSet<IPerson>();
        }

        public int Id { get; set; }

        public virtual Airport Origin { get; set; }

        public virtual Airport Destination { get; set; }

        public virtual Aircraft Aircraft { get; set; }

        public int DurationInMinutes { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public decimal Price { get; set; }

        public int AvailableSeats => this.Aircraft.NumberOfSeats - this.Passengers.Count;

        public int TakenSeats => this.Passengers.Count;

        public virtual ICollection<IPerson> Passengers { get; set; }
    }
}