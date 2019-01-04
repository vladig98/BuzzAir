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
            this.Airports = new HashSet<AirportFlight>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        public ICollection<AirportFlight> Airports { get; set; }

        [Required]
        public int AircraftId { get; set; }
        public Aircraft Aircraft { get; set; }

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

        public ICollection<FlightPassenger> Passengers { get; set; }
    }
}