using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class PersonService
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string PassengerId { get; set; }
        [Required]
        public Passenger Passenger { get; set; }

        [Required]
        public string ServiceId { get; set; }
        [Required]
        public Service Service { get; set; }
    }
}
