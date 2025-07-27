namespace BuzzAir.Models.DbModels
{
    public class TravelDocument
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DocumentType Type { get; set; }

        public required string Number { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public required string NationalityId { get; set; }
        public required Country Nationality { get; set; }

        public required string BirthCountryId { get; set; }
        public required Country BirthCountry { get; set; }

        public required string PassengerId { get; set; }
        public required Passenger Passenger { get; set; }

        public Gender Gender { get; set; }
    }
}