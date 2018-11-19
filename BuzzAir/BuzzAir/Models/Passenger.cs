using System;
using System.Collections.Generic;

namespace BuzzAir.Models
{
    public class Passenger : Person
    {
        public Passenger()
        {
            this.Services = new HashSet<PersonService>();
        }

        public int SeatNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual TravelDocument Document { get; set; }
    }
}