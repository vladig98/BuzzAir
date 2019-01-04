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

        public ICollection<UserBooking> Bookings { get; set; }

        [Required]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public ICollection<PersonService> Services { get; set; }

        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }
    }
}
