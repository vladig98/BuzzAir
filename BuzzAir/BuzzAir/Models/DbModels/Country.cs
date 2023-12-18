using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public class Country
    {
        public Country()
        {
            Nationalities = new HashSet<TravelDocument>();
            BirthCountries = new HashSet<TravelDocument>();
            Cities = new HashSet<City>();
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ISO { get; set; }

        //true for officially recognized countries and false for dependencies
        [Required]
        public bool IsCountry { get; set; }

        public IEnumerable<TravelDocument> Nationalities { get; set; }
        public IEnumerable<TravelDocument> BirthCountries { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}
