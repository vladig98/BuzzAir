using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class Flight
    {
        public Flight()
        {
            this.Passengers = new HashSet<FlightPassenger>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        [Required]
        public virtual Airport Origin { get; set; }

        [Required]
        public virtual Airport Destination { get; set; }

        [Required]
        public virtual Aircraft Aircraft { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int AvailableSeats => this.Aircraft.NumberOfSeats - this.TakenSeats;

        [Required]
        public int TakenSeats => this.Passengers.Count;

        public virtual ICollection<FlightPassenger> Passengers { get; set; }
    }
}