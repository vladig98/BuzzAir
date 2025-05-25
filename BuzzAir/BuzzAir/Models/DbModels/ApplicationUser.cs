namespace BuzzAir.Models.DbModels
{
    public class ApplicationUser : IdentityUser<string>, IPerson, IPassenger
    {
        public ApplicationUser()
        {
            Bookings = new HashSet<UserBooking>();
            Services = new List<PersonService>();
        }

        public ICollection<UserBooking> Bookings { get; set; }

        [Required]
        public string AddressId { get; set; }
        public Address Address { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public IdentityRole Role { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public List<PersonService> Services { get; set; }
    }
}
