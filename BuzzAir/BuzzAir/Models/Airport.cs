using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class Airport
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public virtual Country Country { get; set; }

        [Required]
        public string Terminal { get; set; }
    }
}