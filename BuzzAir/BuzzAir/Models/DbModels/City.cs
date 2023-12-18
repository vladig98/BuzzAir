using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BuzzAir.Models.DbModels
{
    public class City
    {
        public City()
        {
            Addresses = new List<Address>();
            Airports = new List<Airport>();
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public State? State { get; set; }
        public string? StateId { get; set; }

        [JsonIgnore]
        [Required]
        public Country Country { get; set; }
        public string CountryId { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Airport> Airports { get; set; }
    }
}
