namespace BuzzAir.Models.DbModels
{
    public class PersonService
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string PassengerId { get; set; } = string.Empty;
        [Required]
        public Passenger Passenger { get; set; } = new Passenger();
        [Required]
        public string ServiceId { get; set; } = string.Empty;
        [Required]
        public Service Service { get; set; } = new Baggage();
    }
}
