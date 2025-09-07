namespace BuzzAir.DTOs;

public class TravelDocumentDto
{
    public string DocumentType { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string NationalityId { get; set; } = string.Empty;
}
