namespace BuzzAir.Data.Models;

public class ChangeLog
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EntityName { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public ChangeType Action { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime TimestampUTC { get; set; }
    public string? BeforeJSON { get; set; }
    public string? AfterJSON { get; set; }
}
