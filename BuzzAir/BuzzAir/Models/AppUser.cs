using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BuzzAir.Models
{
    public class AppUser : IdentityUser<int>, IPerson
    {
        public AppUser()
        {
            this.Bookings = new HashSet<UserBooking>();
            this.Services = new HashSet<PersonService>();
        }

        public virtual ICollection<UserBooking> Bookings { get; set; }

        public virtual Address Address { get; set; }

        public string FullName { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual ICollection<PersonService> Services { get; set; }
    }
}
