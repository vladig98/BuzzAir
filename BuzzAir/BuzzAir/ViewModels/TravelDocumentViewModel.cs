namespace BuzzAir.ViewModels;

public class TravelDocumentViewModel
{
    public DocumentType Type { get; set; }

    public string Number { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }

    public IList<SelectListItem> Countries { get; } = [];

    public string NationalityId { get; set; } = string.Empty;
    public string BirthCountryId { get; set; } = string.Empty;
}
