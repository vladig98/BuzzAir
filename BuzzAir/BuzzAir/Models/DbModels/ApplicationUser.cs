namespace BuzzAir.Models.DbModels
{
    public class ApplicationUser : IdentityUser<string>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public required City City { get; set; }
        public required string CityId { get; set; }

        public required string PostalCode { get; set; }
        public required string Street { get; set; }

        public string? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
    }
}
