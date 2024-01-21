using BuzzAir.Models.DbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.CreateModels
{
    public class AirportCreateViewModel
    {
        public AirportCreateViewModel()
        {
            CountryOptions = new List<SelectListItem>();
            TimezoneOptions = new List<SelectListItem>();
            Cities = new List<City>();
            States = new List<State>();
        }

        public IEnumerable<SelectListItem> CountryOptions { get; set; }
        public IEnumerable<SelectListItem> TimezoneOptions { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<State> States { get; set; }

        [Required]
        [Display(Prompt = nameof(ICAO))]
        public string ICAO { get; set; }

        [Display(Prompt = nameof(IATA))]
        public string IATA { get; set; }

        [Required]
        [Display(Prompt = nameof(Name))]
        public string Name { get; set; }

        [Display(Prompt = nameof(City))]
        public string City { get; set; }

        [Display(Prompt = nameof(State))]
        public string State { get; set; }

        [Required]
        [Display(Prompt = nameof(Country))]
        public string Country { get; set; }

        [Required]
        [Display(Prompt = nameof(Elevation))]
        public int Elevation { get; set; }

        [Required]
        [Display(Prompt = nameof(Latitude))]
        public double Latitude { get; set; }

        [Required]
        [Display(Prompt = nameof(Longitude))]
        public double Longitude { get; set; }

        [Required]
        [Display(Prompt = nameof(TimeZone))]
        public string TimeZone { get; set; }
    }
}
