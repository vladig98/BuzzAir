using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Models.DbModels
{
    public class Passenger : Person, IPassenger
    {
        public string DocumentId { get; set; } = string.Empty;
        public TravelDocument Document { get; set; } = new TravelDocument();
        [Required]
        public BaggageType BaggageType { get; set; }
        public List<PersonService> Services { get; set; } = [];
    }
}