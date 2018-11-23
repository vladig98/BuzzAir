using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BuzzAir.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser, IPerson
    {
        public ApplicationUser()
        {
            this.Bookings = new HashSet<UserBooking>();
            this.Services = new HashSet<PersonService>();
        }

        //usernam, email, password, phone | Properties from Identity

        public virtual ICollection<UserBooking> Bookings { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public virtual Gender Gender { get; set; }

        public virtual ICollection<PersonService> Services { get; set; }
    }
}
