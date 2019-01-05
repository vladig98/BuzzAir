using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models
{
    public class Service : IService
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public virtual decimal Price { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public int? Kilos { get; set; }

        public SeatType? Type { get; set; }
    }
}
