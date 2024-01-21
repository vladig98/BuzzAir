using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class Aircraft
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}