using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class PersonService
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int PersonId { get; set; }
        [Required]
        public Person Person { get; set; }

        [Required]
        public int ServiceId { get; set; }
        [Required]
        public Service Service { get; set; }
    }
}
