using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class Aircraft
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        public string Name { get; set; }
    }
}