using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class State
    {
        public State()
        {
            Addresses = new List<Address>();
            Airports = new List<Airport>();
            Cities = new List<City>();
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Country Country { get; set; }
        public string CountryId { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Airport> Airports { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
