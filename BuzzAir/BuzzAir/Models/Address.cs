using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class Address
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public virtual Country Country { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Street { get; set; }
    }
}