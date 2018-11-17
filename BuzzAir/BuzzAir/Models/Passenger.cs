using System;
using System.Collections.Generic;

namespace BuzzAir.Models
{
    public class Passenger
    {
        public Passenger()
        {
            this.Services = new HashSet<IService>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int SeatNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual ICollection<IService> Services { get; set; }

        public virtual TravelDocument Document { get; set; }
    }
}