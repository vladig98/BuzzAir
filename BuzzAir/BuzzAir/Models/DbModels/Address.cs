namespace BuzzAir.Models.DbModels
{
    public class Address
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public City City { get; set; }
        public string CityId { get; set; }

        public State? State { get; set; }
        public string? StateId { get; set; }

        [Required]
        public Country Country { get; set; }
        public string CountryId { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Street { get; set; }
    }
}