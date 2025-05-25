namespace BuzzAir.Models.DbModels
{
    public class Aircraft
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
    }
}