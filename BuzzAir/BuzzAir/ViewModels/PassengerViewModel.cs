namespace BuzzAir.ViewModels;

public class PassengerViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public IList<SelectListItem> ServiceOptions { get; } = [];

    public TravelDocumentViewModel? TravelDocument { get; set; }
    public Dictionary<string, bool> Services { get; } = [];
}
