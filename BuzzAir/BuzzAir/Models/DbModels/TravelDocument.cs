namespace BuzzAir.Models.DbModels
{
    public class TravelDocument
    {
        public string Id { get; set; }

        [Required]
        public DocumenType Type { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string NationalityId { get; set; }
        public Country Nationality { get; set; }

        [Required]
        public string BirthCountryId { get; set; }
        public Country BirthCountry { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}