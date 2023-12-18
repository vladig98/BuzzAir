using BuzzAir.Models.DbModels.Contraccts;
using BuzzAir.Models.DbModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class Passenger : Person, IPassenger
    {
        public Passenger()
        {
            Services = new List<PersonService>();
        }

        public string DocumentId { get; set; }

        public TravelDocument Document { get; set; }

        [Required]
        public BaggageType BaggageType { get; set; }

        public List<PersonService> Services { get; set; }
    }
}