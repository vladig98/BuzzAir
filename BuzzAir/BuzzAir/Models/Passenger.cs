using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class Passenger : Person
    {
        public Passenger()
        {
            this.Services = new HashSet<PersonService>();
        }

        [Required]
        public int SeatNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int DocumentId { get; set; }
        public TravelDocument Document { get; set; }
    }
}