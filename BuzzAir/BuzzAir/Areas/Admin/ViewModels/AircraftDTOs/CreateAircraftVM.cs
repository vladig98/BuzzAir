namespace BuzzAir.Areas.Admin.ViewModels.AircraftDTOs
{
    public class CreateAircraftVM
    {
        [Required]
        [RegularExpression("[a-zA-Z0-9-]{3,}")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Prompt = "Number of seats")]
        [Range(GlobalConstants.MinimumNumberOfSeatsForAnAircraft, GlobalConstants.MaximumNumberOfSeatsForAnAircraft)]
        public int Seats { get; set; }
    }
}
