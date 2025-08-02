namespace BuzzAir.Data.Models;

public class ApplicationUser : IdentityUser<string>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }

    public City City { get; set; } = null!;
    public string CityId { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;

    public string? PassengerId { get; set; }
    public Passenger? Passenger { get; set; }
}
