using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BuzzAir.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Bookings = new HashSet<Booking>();
        }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
