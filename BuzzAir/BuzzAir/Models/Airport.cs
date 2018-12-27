using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class Airport
    {
        public Airport()
        {
            this.Flights = new HashSet<AirportFlight>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public virtual Country Country { get; set; }

        [Required]
        public string Terminal { get; set; }

        [Required]
        public virtual ICollection<AirportFlight> Flights { get; set; }
    }
}