namespace BuzzAir.Data.Models;

public class TravelDocument
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DocumentType Type { get; set; }

    public string Number { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    public string NationalityId { get; set; } = string.Empty;
    public Country Nationality { get; set; } = null!;

    public string BirthCountryId { get; set; } = string.Empty;
    public Country BirthCountry { get; set; } = null!;

    public string PassengerId { get; set; } = string.Empty;
    public Passenger Passenger { get; set; } = null!;

    public Gender Gender { get; set; }
}