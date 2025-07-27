namespace BuzzAir.Models.DbModels
{
    public class ChangeLog
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string EntityName { get; set; }
        public required string EntityId { get; set; }
        public ChangeType Action { get; set; }
        public required string UserId { get; set; }
        public DateTime TimestampUTC { get; set; }
        public string? BeforeJSON { get; set; }
        public string? AfterJSON { get; set; }
    }
}
