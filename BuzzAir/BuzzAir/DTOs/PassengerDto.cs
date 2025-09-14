namespace BuzzAir.DTOs;

public class PassengerDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public TravelDocumentDto? TravelDocument { get; set; }
    public IList<string> ServiceIds { get; } = [];
}
